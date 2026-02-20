using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Shared;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Aggregates.Courses.ValueObjects
{
    public sealed class CourseDescription : ValueObject
    {
        public string Value { get; private set; }

        private CourseDescription(string value) => Value = value;

        public static Result<CourseDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result<CourseDescription>.Failure(Error.NullValue);

            if (description.Length < 20)
                return Result<CourseDescription>.Failure(CourseErrors.DescriptionTooShort);

            return Result<CourseDescription>.Success(new CourseDescription(description));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
