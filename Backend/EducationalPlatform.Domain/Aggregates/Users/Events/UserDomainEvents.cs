using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Aggregates.Users.Events;

 
public sealed record UserRegisteredDomainEvent(
    Guid UserId,
    string Email,
    string FullName) : DomainEvent;

public sealed record UserDeactivatedDomainEvent(
    Guid UserId,
    string Reason) : DomainEvent;
