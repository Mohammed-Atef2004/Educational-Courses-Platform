using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Courses.ValueObjects
{
    public sealed record CourseId(Guid Value)
    {
        public static CourseId Empty => new(Guid.Empty);
        public static CourseId New() => new(Guid.NewGuid());
        public static CourseId From(Guid id) => new(id);
        public static CourseId From(string id) => new(Guid.Parse(id));
        public override string ToString() => Value.ToString();
    }
}
