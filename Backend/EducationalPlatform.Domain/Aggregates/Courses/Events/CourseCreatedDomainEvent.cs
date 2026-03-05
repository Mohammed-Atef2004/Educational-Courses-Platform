using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Aggregates.Courses.Events;


public sealed record CourseCreatedDomainEvent(
    Guid   CourseId,
    string CourseName,
    decimal Price,
    string Currency) : DomainEvent;


public sealed record CoursePriceUpdatedDomainEvent(
    Guid    CourseId,
    decimal OldPrice,
    decimal NewPrice,
    string  Currency) : DomainEvent;


public sealed record EpisodeAddedDomainEvent(
    Guid CourseId,
    Guid EpisodeId,
    string EpisodeName) : DomainEvent;


public sealed record CoursePublishedDomainEvent(
    Guid   CourseId,
    string CourseName) : DomainEvent;
