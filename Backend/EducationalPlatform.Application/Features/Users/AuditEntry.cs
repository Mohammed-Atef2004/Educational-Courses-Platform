using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users
{
    public sealed record AuditEntry(
     Guid ActorId,        
     string Action,          
     string EntityType,       
     Guid EntityId,         
     string? IpAddress,        
     bool Succeeded,
     string? FailureReason = null,
     DateTime? OccurredAt = null)
    {
        public DateTime OccurredAtUtc => OccurredAt ?? DateTime.UtcNow;
    }
}
