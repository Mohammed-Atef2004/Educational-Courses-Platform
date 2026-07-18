using EducationalPlatform.Domain.Users;
using EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.ChangeRole
{
    public sealed record ChangeRoleCommand(
    Guid TargetUserId,
    Guid ActorId,
    UserRole NewRole
) : IRequest<Result>;
}
