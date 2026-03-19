
using  EducationalPlatform.Domain.Users.Errors;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace  EducationalPlatform.Domain.Users.ValueObjects
{
    public sealed class Username : ValueObject
    {
        public const int MinLength = 3;
        public const int MaxLength = 30;

        // letters, digits, underscore, hyphen — no spaces, no dots
        private static readonly Regex UsernameRegex = new(
            @"^[a-zA-Z0-9_\-]+$",
            RegexOptions.Compiled,
            matchTimeout: TimeSpan.FromMilliseconds(100));

        public string Value { get; private set; }

        private Username() { }
        private Username(string value) => Value = value;

        public static Result<Username> Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<Username>.Failure(UsernameErrors.Empty);

            var trimmed = value.Trim();

            if (trimmed.Length < MinLength)
                return Result<Username>.Failure(UsernameErrors.TooShort);

            if (trimmed.Length > MaxLength)
                return Result<Username>.Failure(UsernameErrors.TooLong);

            if (!UsernameRegex.IsMatch(trimmed))
                return Result<Username>.Failure(UsernameErrors.InvalidFormat);

            // stored in lowercase for case-insensitive uniqueness checks
            return Result<Username>.Success(new Username(trimmed.ToLowerInvariant()));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value; // already lowercase
        }

        public override string ToString() => Value;
    }
}
