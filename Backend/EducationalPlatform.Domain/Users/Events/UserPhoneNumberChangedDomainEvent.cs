using EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Users.Events
{
    public sealed record UserPhoneNumberChangedDomainEvent(
    Guid UserId,
    string? OldPhoneNumber,   // null = was not set
    string? NewPhoneNumber    // null = was removed
) : DomainEvent;
}
