using EducationalPlatform.EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.EducationalPlatform.Domain.Courses.Events
{
    public sealed record CoursePriceUpdatedDomainEvent(
    CourseId CourseId,
    decimal OldPrice,
    decimal NewPrice,
    string Currency) : DomainEvent;
}
