using EducationalPlatform.Domain.SharedKernel;
using EducationalPlatform.Domain.Users;
namespace EducationalPlatform.Domain.Users.ValueObjects;


public sealed class FullName : ValueObject
{
    public const int MaxLength = 100;

    public string FirstName { get; }
    public string LastName  { get; }
    public string DisplayName => $"{FirstName} {LastName}";

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName  = lastName;
    }

    public static Result<FullName> Create(string? firstName, string? lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result<FullName>.Failure(UserErrors.FullName.FirstNameEmpty);

        if (string.IsNullOrWhiteSpace(lastName))
            return Result<FullName>.Failure(UserErrors.FullName.LastNameEmpty);

        var first = firstName.Trim();
        var last  = lastName.Trim();

        if (first.Length > MaxLength)
            return Result<FullName>.Failure(UserErrors.FullName.FirstNameTooLong);

        if (last.Length > MaxLength)
            return Result<FullName>.Failure(UserErrors.FullName.LastNameTooLong);

        return Result<FullName>.Success(new FullName(first, last));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName.ToLowerInvariant();
        yield return LastName.ToLowerInvariant();
    }

    public override string ToString() => DisplayName;
}
