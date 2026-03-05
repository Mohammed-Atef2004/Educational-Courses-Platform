using EducationalPlatform.Domain.Aggregates.Users.ValueObjects;
using EducationalPlatform.Domain.SharedKernel;
using EducationalPlatform.Domain.Aggregates.Users.Events;
using EducationalPlatform.Domain.Aggregates.Users.Rules;

namespace EducationalPlatform.Domain.Aggregates.Users;


public sealed class User : AggregateRoot<Guid>
{
    public string IdentityId { get; private set; }

    //  Value Objects
    public Email Email { get; private set; }
    public FullName FullName { get; private set; }

    // Domain state 

    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    // use the factory 
    private User(Guid id, string identityId,Email email,FullName fullName,UserRole role): base(id)
    {
        IdentityId = identityId;
        Email = email;
        FullName = fullName;
        Role = role;
        IsActive = true;
        RegisteredAt = DateTime.UtcNow;
    }

    private User() { }
    public static Result<User> Create(
        string identityId,
        string email,
        string firstName,
        string lastName,
        bool isEmailTaken,
        UserRole role = UserRole.Student)
    {
        //  Identity 
        if (string.IsNullOrWhiteSpace(identityId))
            return Result<User>.Failure(UserErrors.InvalidIdentityReference);

        // Email Value Object 
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
            return Result<User>.Failure(emailResult.Error);

        // FullName Value Object
        var fullNameResult = FullName.Create(firstName, lastName);
        if (fullNameResult.IsFailure)
            return Result<User>.Failure(fullNameResult.Error);

        // Business Rule  email uniqueness :(
        var user = new User(Guid.NewGuid(), identityId, emailResult.Value, fullNameResult.Value, role);

        var ruleResult = user.CheckRule(new UserEmailMustBeUniqueRule(isEmailTaken));
        if (ruleResult.IsFailure)
            return Result<User>.Failure(ruleResult.Error);

    
        user.AddDomainEvent(new UserRegisteredDomainEvent(
            user.Id,
            user.Email.Value,
            user.FullName.DisplayName));
        return Result<User>.Success(user);
    }


 
    public Result ChangeName(string firstName, string lastName)
    {
        if (IsDeleted || !IsActive)
            return Result.Failure(UserErrors.Deactivated);

        var fullNameResult = FullName.Create(firstName, lastName);
        if (fullNameResult.IsFailure)
            return Result.Failure(fullNameResult.Error);

        FullName = fullNameResult.Value;
        return Result.Success();
    }


    public Result Deactivate(string reason)
    {
        if (!IsActive)
            return Result.Failure(UserErrors.Deactivated);

        IsActive = false;
        AddDomainEvent(new UserDeactivatedDomainEvent(Id, reason));

        return Result.Success();
    }

    public Result PromoteToInstructor()
    {
        if (!IsActive)
            return Result.Failure(UserErrors.Deactivated);

        Role = UserRole.Instructor;
        return Result.Success();
    }
}
