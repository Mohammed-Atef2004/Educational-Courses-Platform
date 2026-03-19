using EducationalPlatform.EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.EducationalPlatform.Domain.Courses.Events;


public sealed record CourseCreatedDomainEvent(
    CourseId   CourseId,
    string CourseName,
    decimal Price,
    string Currency) : DomainEvent;






