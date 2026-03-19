
using  EducationalPlatform.Domain.Users.Errors;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System.Text.RegularExpressions;
namespace  EducationalPlatform.Domain.Users.ValueObjects;


public sealed class FullName : ValueObject
{
    public const int MaxLength = 100;

    // letters (Unicode), hyphens, apostrophes — no digits or symbols
    private static readonly Regex NameRegex = new(
        @"^[\p{L}\s'\-]+$",
        RegexOptions.Compiled,
        matchTimeout: TimeSpan.FromMilliseconds(100));

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string DisplayName => $"{FirstName} {LastName}";

    private FullName() { }
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<FullName> Create(string? firstName, string? lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result<FullName>.Failure(FullNameErrors.FirstNameEmpty);

        if (string.IsNullOrWhiteSpace(lastName))
            return Result<FullName>.Failure(FullNameErrors.LastNameEmpty);

        var first = firstName.Trim();
        var last = lastName.Trim();

        if (first.Length > MaxLength)
            return Result<FullName>.Failure(FullNameErrors.FirstNameTooLong);

        if (last.Length > MaxLength)
            return Result<FullName>.Failure(FullNameErrors.LastNameTooLong);

        if (!NameRegex.IsMatch(first) || !NameRegex.IsMatch(last))
            return Result<FullName>.Failure(FullNameErrors.InvalidCharacters);

        return Result<FullName>.Success(new FullName(first, last));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName.ToLowerInvariant();
        yield return LastName.ToLowerInvariant();
    }

    public override string ToString() => DisplayName;
}

