
using  EducationalPlatform.Domain.Users.Errors;
using EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace  EducationalPlatform.Domain.Users.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public const int MaxDigits = 15; // ITU-T E.164 standard

        // Must start with '+' followed by 7–15 digits
        private static readonly Regex PhoneRegex = new(
            @"^\+[1-9]\d{6,14}$",
            RegexOptions.Compiled,
            matchTimeout: TimeSpan.FromMilliseconds(100));

        public string Value { get; private set; }

        private PhoneNumber(){}
        private PhoneNumber(string value) => Value = value;

        public static Result<PhoneNumber> Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<PhoneNumber>.Failure(PhoneNumberErrors.Empty);

            // Strip spaces and dashes that users commonly type
            var normalized = value.Trim().Replace(" ", "").Replace("-", "");

            if (normalized.Length > MaxDigits + 1) // +1 for the '+' prefix
                return Result<PhoneNumber>.Failure(PhoneNumberErrors.TooLong);

            if (!PhoneRegex.IsMatch(normalized))
                return Result<PhoneNumber>.Failure(PhoneNumberErrors.InvalidFormat);

            return Result<PhoneNumber>.Success(new PhoneNumber(normalized));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
