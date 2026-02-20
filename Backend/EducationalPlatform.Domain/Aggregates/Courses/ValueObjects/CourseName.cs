using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Shared;

namespace EducationalPlatform.Domain.Aggregates.Courses.ValueObjects
{
    public sealed class CourseName : ValueObject
    {
        public string Value { get; private set; }

        private CourseName(string value) => Value = value;

        public static Result<CourseName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result<CourseName>.Failure(CourseErrors.EmptyName);

            if (name.Length > 100)
                return Result<CourseName>.Failure(new Error("CourseName.TooLong", "Name is too long."));

            return Result<CourseName>.Success(new CourseName(name));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
