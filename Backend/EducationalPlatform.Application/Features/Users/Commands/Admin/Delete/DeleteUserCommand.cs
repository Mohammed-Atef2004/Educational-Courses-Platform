using EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.Delete
{
    public sealed record DeleteUserCommand(
    Guid UserId,
    Guid ActorId,
    string Reason
) : IRequest<Result>;
}
