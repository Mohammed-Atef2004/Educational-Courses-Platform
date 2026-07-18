using EducationalPlatform.Domain.SharedKernel;
namespace  EducationalPlatform.Domain.Users.Events
{
    public sealed record UserTwoFactorDisabledDomainEvent(
     Guid UserId) : DomainEvent;
}
