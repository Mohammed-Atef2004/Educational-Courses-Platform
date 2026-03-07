using EducationalPlatform.Domain.SharedKernel;
using EducationalPlatform.Domain.Users.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EducationalPlatform.Domain.Users.Errors.UserErrors;

namespace EducationalPlatform.Domain.Users.Rules
{
    public sealed class PasswordMustNotBeReusedRule : IBusinessRule
    {
        public const int HistoryDepth = 5;

        private readonly bool _isPasswordReused;

        public PasswordMustNotBeReusedRule(bool isPasswordReused)
            => _isPasswordReused = isPasswordReused;

        public bool IsBroken() => _isPasswordReused;
        public Error Error => SecurityErrors.PasswordAlreadyUsed;
    }
}
