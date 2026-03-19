using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.UnlockAccount
{
    public sealed record UnlockAccountCommand(Guid UserId, Guid ActorId) : IRequest<Result>;
}
