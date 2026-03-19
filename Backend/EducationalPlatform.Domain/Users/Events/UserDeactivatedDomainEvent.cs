using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EducationalPlatform.Domain.Users.Events
{
    public sealed record UserDeactivatedDomainEvent(
       Guid UserId,
       string Reason) : DomainEvent;
}
