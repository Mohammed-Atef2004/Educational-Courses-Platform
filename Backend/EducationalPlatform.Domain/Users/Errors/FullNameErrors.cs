using EducationalPlatform.Domain.SharedKernel;
using EducationalPlatform.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Users.Errors
{
    public static class FullNameErrors
    {
        public static readonly Error FirstNameEmpty =
            new("User.FullName.FirstNameEmpty", "First name cannot be empty.");

        public static readonly Error LastNameEmpty =
            new("User.FullName.LastNameEmpty", "Last name cannot be empty.");

        public static readonly Error FirstNameTooLong =
            new("User.FullName.FirstNameTooLong",
                $"First name must not exceed {FullName.MaxLength} characters.");

        public static readonly Error LastNameTooLong =
            new("User.FullName.LastNameTooLong",
                $"Last name must not exceed {FullName.MaxLength} characters.");

        public static readonly Error InvalidCharacters =
            new("User.FullName.InvalidCharacters",
                "Name must contain letters only (hyphens and apostrophes are allowed).");
    }
}
