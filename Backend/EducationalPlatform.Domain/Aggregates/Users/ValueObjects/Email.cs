using System.Text.RegularExpressions;
using EducationalPlatform.Domain.Aggregates.Users;
using EducationalPlatform.Domain.SharedKernel;
namespace EducationalPlatform.Domain.Aggregates.Users.ValueObjects;


public sealed class Email : ValueObject
{
   
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase,
        matchTimeout: TimeSpan.FromMilliseconds(250));

    public string Value { get; }

    private Email(string value) => Value = value;


    public static Result<Email> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Email>.Failure(UserErrors.Email.Empty);

        var trimmed = value.Trim();

        if (trimmed.Length > 254) 
            return Result<Email>.Failure(UserErrors.Email.TooLong);

        if (!EmailRegex.IsMatch(trimmed))
            return Result<Email>.Failure(UserErrors.Email.InvalidFormat);

        return Result<Email>.Success(new Email(trimmed.ToLowerInvariant()));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
