using EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Security.Disable2FA
{
    public sealed record Disable2FACommand(Guid UserId) : IRequest<Result>;

}
