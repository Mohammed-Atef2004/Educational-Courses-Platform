using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.Deactivate
{
    public sealed record DeactivateUserCommand(
     Guid UserId,
     Guid ActorId,
     string Reason
 ) : IRequest<Result>;
}
