using EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Courses.Events;


public sealed record CourseCreatedDomainEvent(
    CourseId CourseId,
    Guid InstrucorId,
    string CourseName,
    decimal Price,
    string Currency) : DomainEvent;






