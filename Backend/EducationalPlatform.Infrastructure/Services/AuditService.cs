
using EducationalPlatform.Application.Features.Users;
using EducationalPlatform.Application.Features.Users.Services;
using EducationalPlatform.Infrastructure.Presistence.Entities;
using Infrastructure.Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public sealed class AuditService : IAuditService
    {
        private readonly AppDbContext _db;

        public AuditService(AppDbContext db) => _db = db;

        public async Task LogAsync(AuditEntry entry, CancellationToken ct = default)
        {
            var log = AuditLog.Create(
                actorId: entry.ActorId,
                action: entry.Action,
                entityType: entry.EntityType,
                entityId: entry.EntityId,
                ipAddress: entry.IpAddress,
                succeeded: entry.Succeeded,
                failureReason: entry.FailureReason);

            await _db.AuditLogs.AddAsync(log, ct);
            await _db.SaveChangesAsync(ct);
        }
    }
}
