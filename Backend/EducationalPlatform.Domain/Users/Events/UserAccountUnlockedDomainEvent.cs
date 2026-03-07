using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Users.Events
{
    public sealed record UserAccountUnlockedDomainEvent(
    Guid UserId) : DomainEvent;
}
