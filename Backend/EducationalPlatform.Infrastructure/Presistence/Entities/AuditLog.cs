using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Presistence.Entities
{
    /// <summary>
    /// Infrastructure Entity — Not a Domain Entity
    /// can be accessed throw audiutserivce only 
    /// </summary>
    public sealed class AuditLog
    {
        public long Id { get; private set; }  // auto-increment
        public Guid ActorId { get; private set; }  // Who Did The Action
        public string Action { get; private set; } = default!;  // what is the action
        public string EntityType { get; private set; } = default!;  // on what action 
        public Guid EntityId { get; private set; }  // ID
        public string? IpAddress { get; private set; }  // from where
        public bool Succeeded { get; private set; }  
        public string? FailureReason { get; private set; }  
        public DateTime OccurredAt { get; private set; }  

        private AuditLog() { }  // EF Core constructor

        public static AuditLog Create(
            Guid actorId,
            string action,
            string entityType,
            Guid entityId,
            string? ipAddress,
            bool succeeded,
            string? failureReason = null) => new()
            {
                ActorId = actorId,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                IpAddress = ipAddress,
                Succeeded = succeeded,
                FailureReason = failureReason,
                OccurredAt = DateTime.UtcNow
            };
    }
}
