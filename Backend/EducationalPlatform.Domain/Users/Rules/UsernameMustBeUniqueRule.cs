using  EducationalPlatform.Domain.Users.Errors;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EducationalPlatform.Domain.Users.Rules
{
    public sealed class UsernameMustBeUniqueRule : IBusinessRule
    {
        private readonly bool _isUsernameTaken;

        public UsernameMustBeUniqueRule(bool isUsernameTaken)
            => _isUsernameTaken = isUsernameTaken;

        public bool IsBroken() => _isUsernameTaken;
        public Error Error => RulesErrors.UsernameMustBeUnique;
    }
}
