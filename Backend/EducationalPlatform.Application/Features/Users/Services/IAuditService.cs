using EducationalPlatform.Application.Features.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Services
{
    public interface IAuditService
    {
        Task LogAsync(AuditEntry entry, CancellationToken ct = default);
    }
}
