using EducationalPlatform.Domain.SharedKernel;
using EducationalPlatform.Domain.Users.Errors;
using EducationalPlatform.Domain.Users.Events;
using EducationalPlatform.Domain.Users.ValueObjects;

namespace EducationalPlatform.Domain.Users;

public sealed class User : AggregateRoot<Guid>
{
    public string IdentityId { get; private set; } = default!;

    public FullName FullName { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public PhoneNumber? PhoneNumber { get; private set; }

    public string? ImageUrl { get; private set; }

    public UserRole Role { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime RegisteredAt { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public string? DeleteReason { get; private set; }

    private User()
    {
    }

    private User(
        Guid id,
        string identityId,
        FullName fullName,
        Email email,
        UserRole role)
        : base(id)
    {
        IdentityId = identityId;
        FullName = fullName;
        Email = email;
        Role = role;

        IsActive = true;
        IsDeleted = false;
        RegisteredAt = DateTime.UtcNow;
    }

    public static Result<User> Create(
        string identityId,
        string firstName,
        string lastName,
        string email,
        UserRole role = UserRole.Student)
    {
        if (string.IsNullOrWhiteSpace(identityId))
            return Result<User>.Failure(UserErrors.InvalidIdentityReference);

        var fullNameResult = FullName.Create(firstName, lastName);
        if (fullNameResult.IsFailure)
            return Result<User>.Failure(fullNameResult.Error);

        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
            return Result<User>.Failure(emailResult.Error);

        var user = new User(
            Guid.NewGuid(),
            identityId,
            fullNameResult.Value,
            emailResult.Value,
            role);

        user.AddDomainEvent(new UserRegisteredDomainEvent(
            user.Id,
            user.Email.Value,
            user.FullName.DisplayName));

        return Result<User>.Success(user);
    }

    public Result ChangeFullName(string firstName, string lastName)
    {
        var result = FullName.Create(firstName, lastName);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        FullName = result.Value;

        return Result.Success();
    }

    public Result UpdatePhoneNumber(string phoneNumber)
    {
        var result = PhoneNumber.Create(phoneNumber);

        if (result.IsFailure)
            return Result.Failure(result.Error);

        PhoneNumber = result.Value;

        return Result.Success();
    }

    public Result RemovePhoneNumber()
    {
        PhoneNumber = null;

        return Result.Success();
    }

    public Result UpdateImage(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            return Result.Failure(UserErrors.InvalidImage);

        ImageUrl = imageUrl;

        return Result.Success();
    }

    public Result ChangeRole(UserRole role)
    {
        if (Role == role)
            return Result.Failure(UserErrors.RoleAlreadyAssigned);

        Role = role;

        return Result.Success();
    }

    public Result Delete(string reason)
    {
        if (IsDeleted)
            return Result.Failure(UserErrors.AlreadyDeleted);

        IsDeleted = true;
        IsActive = false;
        DeletedAt = DateTime.UtcNow;
        DeleteReason = reason;

        AddDomainEvent(new UserDeletedDomainEvent(Id));

        return Result.Success();
    }
}