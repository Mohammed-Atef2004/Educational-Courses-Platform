using EducationalPlatform.Domain.Interfaces.Repositories;
using EducationalPlatform.Domain.Users;
using EducationalPlatform.Domain.Users.Errors;
using EducationalPlatform.Application.Features.Users;
using EducationalPlatform.Application.Features.Users.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationalPlatform.Domain.SharedKernel;
using Domain.Interfaces.Services;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.Deactivate
{
    public sealed class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Result>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokens;
        private readonly IAuditService _audit;

        public DeactivateUserCommandHandler(
            IUserRepository users, IUnitOfWork uow,
            ITokenService tokens, IAuditService audit)
        {
            _users = users; _uow = uow; _tokens = tokens; _audit = audit;
        }

        public async Task<Result> Handle(DeactivateUserCommand command, CancellationToken ct)
        {
            var user = _users.EntityQuery.FirstOrDefault(x => x.Id == command.UserId);
            if (user is null) return Result.Failure(UserErrors.NotFound);

            var result = user.Deactivate(command.Reason);
            if (result.IsFailure) return result;

           // await _tokens.RevokeAllRefreshTokensAsync(user.Id, ct);

            _users.Update(user);
            await _uow.CompleteAsync(ct);

            await _audit.LogAsync(new AuditEntry(
                ActorId: command.ActorId, Action: AuditActions.UserDeactivated,
                EntityType: nameof(User), EntityId: user.Id,
                IpAddress: null, Succeeded: true), ct);

            return Result.Success();
        }
    }
}
