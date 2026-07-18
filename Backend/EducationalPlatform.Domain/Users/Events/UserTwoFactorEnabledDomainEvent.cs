using EducationalPlatform.Domain.SharedKernel;
namespace  EducationalPlatform.Domain.Users.Events
{
    public sealed record UserTwoFactorEnabledDomainEvent(
    Guid UserId) : DomainEvent;
}
