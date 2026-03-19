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
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using Domain.Interfaces.Services;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.Delete
{
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokens;
        private readonly IAuditService _audit;

        public DeleteUserCommandHandler(
            IUserRepository users, IUnitOfWork uow,
            ITokenService tokens, IAuditService audit)
        {
            _users = users; _uow = uow; _tokens = tokens; _audit = audit;
        }

        public async Task<Result> Handle(DeleteUserCommand command, CancellationToken ct)
        {
            var user = _users.EntityQuery.FirstOrDefault(x => x.Id == command.UserId);
            if (user is null) return Result.Failure(UserErrors.NotFound);

            var result = user.Delete(command.Reason);
            if (result.IsFailure) return result;

            //await _tokens.RevokeAllRefreshTokensAsync(user.Id, ct);
            _users.SoftDelete(user);
            await _uow.CompleteAsync(ct);

            await _audit.LogAsync(new AuditEntry(
                ActorId: command.ActorId, Action: AuditActions.UserDeleted,
                EntityType: nameof(User), EntityId: user.Id,
                IpAddress: null, Succeeded: true), ct);

            return Result.Success();
        }
    }
}
