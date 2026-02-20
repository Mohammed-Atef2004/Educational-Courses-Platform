using EducationalPlatform.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Aggregates.Courses.Events
{
    public sealed record CourseCreatedDomainEvent(Guid CourseId) : IDomainEvent;
}
