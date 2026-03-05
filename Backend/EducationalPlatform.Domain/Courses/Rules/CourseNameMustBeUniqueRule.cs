using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Courses.Rules
{
    public sealed class CourseNameMustBeUniqueRule : IBusinessRule
    {
        private readonly bool _isTaken;

        public CourseNameMustBeUniqueRule(bool isTaken) => _isTaken = isTaken;

        public bool IsBroken() => _isTaken;
        public Error Error => CourseErrors.Rules.CourseNameMustBeUnique;
    }

}
