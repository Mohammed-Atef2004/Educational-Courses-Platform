using  EducationalPlatform.Domain.Users.ValueObjects;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EducationalPlatform.Domain.Users.Errors
{
    public static class UsernameErrors
    {
        public static readonly Error Empty =
            new("User.Username.Empty", "Username cannot be empty.");

        public static readonly Error TooShort =
            new("User.Username.TooShort",
                $"Username must be at least {Username.MinLength} characters.");

        public static readonly Error TooLong =
            new("User.Username.TooLong",
                $"Username must not exceed {Username.MaxLength} characters.");

        public static readonly Error InvalidFormat =
            new("User.Username.InvalidFormat",
                "Username may only contain letters, digits, underscores, and hyphens.");

        public static readonly Error AlreadyTaken =
            new("User.Username.AlreadyTaken", "This username is already taken.");
    }

}
