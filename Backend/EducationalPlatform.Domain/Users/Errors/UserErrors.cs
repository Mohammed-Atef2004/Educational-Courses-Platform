using EducationalPlatform.Domain.SharedKernel;
using EducationalPlatform.Domain.Users.ValueObjects;

namespace EducationalPlatform.Domain.Users.Errors;

public static class UserErrors
{
    public static readonly Error NotFound =
        new("User.NotFound", "The requested user does not exist.");

    public static readonly Error AlreadyExists =
        new("User.AlreadyExists", "A user with this email address already exists.");

    public static readonly Error Deactivated =
        new("User.Deactivated", "This user account has been deactivated.");

    public static readonly Error AlreadyDeleted =
        new("User.AlreadyDeleted", "This user account has already been deleted.");

    public static readonly Error AlreadyActive =
        new("User.AlreadyActive", "This user account is already active.");

    public static readonly Error InvalidIdentityReference =
        new("User.InvalidIdentityReference", "The provided identity ID is null or empty.");

    public static readonly Error CannotDeleteSuperAdmin =
        new("User.CannotDeleteSuperAdmin", "SuperAdmin accounts cannot be deleted.");

    public static readonly Error CannotDeactivateSuperAdmin =
        new("User.CannotDeactivateSuperAdmin", "SuperAdmin accounts cannot be deactivated.");

    public static readonly Error RoleAlreadyAssigned =
        new("User.RoleAlreadyAssigned", "The user already has this role.");

    public static readonly Error CannotAssignSuperAdminRole =
        new("User.CannotAssignSuperAdminRole", "SuperAdmin role cannot be assigned through normal operations.");

    

   

    
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
    }

}