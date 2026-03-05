using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Aggregates.Users;


public static class UserErrors
{

    public static readonly Error NotFound =
        new("User.NotFound", "The requested user does not exist.");

    public static readonly Error AlreadyExists =
        new("User.AlreadyExists", "A user with this email address already exists.");

    public static readonly Error Deactivated =
        new("User.Deactivated", "This user account has been deactivated.");

    public static readonly Error InvalidIdentityReference =
        new("User.InvalidIdentityReference", "The provided identity ID is null or empty.");

    // Email Value Object errors
    public static class Email
    {
        public static readonly Error Empty =
            new("User.Email.Empty", "Email address cannot be empty.");

        public static readonly Error TooLong =
            new("User.Email.TooLong", "Email address must not exceed 254 characters.");

        public static readonly Error InvalidFormat =
            new("User.Email.InvalidFormat", "Email address format is invalid.");
    }

    // FullName Value Object errors

    public static class FullName
    {
        public static readonly Error FirstNameEmpty =
            new("User.FullName.FirstNameEmpty", "First name cannot be empty.");

        public static readonly Error LastNameEmpty =
            new("User.FullName.LastNameEmpty", "Last name cannot be empty.");

        public static readonly Error FirstNameTooLong =
            new("User.FullName.FirstNameTooLong",
                $"First name must not exceed {EducationalPlatform.Domain.Aggregates.Users.ValueObjects.FullName.MaxLength} characters.");

        public static readonly Error LastNameTooLong =
            new("User.FullName.LastNameTooLong",
                $"Last name must not exceed {EducationalPlatform.Domain.Aggregates.Users.ValueObjects.FullName.MaxLength} characters.");
    }

    // Business rule violation errors 

    public static class Rules
    {
        public static readonly Error EmailMustBeUnique =
            new("User.Rules.EmailMustBeUnique",
                "An active account already exists with this email address.");
    }
}
