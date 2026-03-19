using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EducationalPlatform.Domain.Users.Errors
{
    public static class PhoneNumberErrors
    {
        public static readonly Error Empty =
            new("User.PhoneNumber.Empty", "Phone number cannot be empty.");

        public static readonly Error InvalidFormat =
            new("User.PhoneNumber.InvalidFormat",
                "Phone number must be in E.164 format (e.g. +20123456789).");

        public static readonly Error TooLong =
            new("User.PhoneNumber.TooLong", "Phone number must not exceed 15 digits.");
    }
}
