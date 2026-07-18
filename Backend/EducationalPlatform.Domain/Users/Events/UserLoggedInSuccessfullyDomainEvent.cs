
using EducationalPlatform.Domain.SharedKernel;

namespace  EducationalPlatform.Domain.Users.Events
{
    public sealed record UserLoggedInSuccessfullyDomainEvent(
     Guid UserId,
     string IpAddress,
     DateTime LoginAt) : DomainEvent;
}
