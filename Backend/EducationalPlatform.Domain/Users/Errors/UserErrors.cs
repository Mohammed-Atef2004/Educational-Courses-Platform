using  EducationalPlatform.Domain.Users.ValueObjects;
using EducationalPlatform.Domain.SharedKernel;

namespace  EducationalPlatform.Domain.Users.Errors;

public static partial class UserErrors
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

    public static readonly Error InvalidImage =
        new("User.InvalidImage", "Image URL is Empty");


}