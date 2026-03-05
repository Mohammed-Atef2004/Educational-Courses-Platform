using EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Courses.Events
{
    public sealed record CoursePublishedDomainEvent(
    CourseId CourseId,
    string CourseName) : DomainEvent;
}
