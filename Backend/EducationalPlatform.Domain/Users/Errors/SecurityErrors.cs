using EducationalPlatform.Domain.SharedKernel;

namespace  EducationalPlatform.Domain.Users.Errors;

public static partial class UserErrors
{
    public static class SecurityErrors
    {
        public static readonly Error AccountLocked =
            new("User.Security.AccountLocked",
                "Account is temporarily locked due to too many failed login attempts.");

        public static readonly Error TwoFactorAlreadyEnabled =
            new("User.Security.TwoFactorAlreadyEnabled", "Two-factor authentication is already enabled.");

        public static readonly Error TwoFactorNotEnabled =
            new("User.Security.TwoFactorNotEnabled", "Two-factor authentication is not enabled.");

        public static readonly Error InvalidTwoFactorSecret =
            new("User.Security.InvalidTwoFactorSecret", "Two-factor secret cannot be empty.");

        public static readonly Error PasswordAlreadyUsed =
            new("User.Security.PasswordAlreadyUsed",
                "This password was used recently. Please choose a different password.");
        public static readonly Error InvalidEmailToken
            = new("User.Security.InvalidEmailToken", "The provided email token is invalid or expired.");
        public static readonly Error InvalidTotpCode
            = new("User.Security.InvalidTotpCode", "The provided TotpCode is Invalid");
    }

}