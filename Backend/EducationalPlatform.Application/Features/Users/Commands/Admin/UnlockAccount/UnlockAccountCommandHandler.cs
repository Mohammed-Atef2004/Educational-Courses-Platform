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

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.UnlockAccount
{
    public sealed class UnlockAccountCommandHandler : IRequestHandler<UnlockAccountCommand, Result>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _uow;
        private readonly IAuditService _audit;

        public UnlockAccountCommandHandler(IUserRepository users, IUnitOfWork uow, IAuditService audit)
        {
            _users = users; _uow = uow; _audit = audit;
        }

        public async Task<Result> Handle(UnlockAccountCommand command, CancellationToken ct)
        {
            var user =  _users.EntityQuery.FirstOrDefault(x=>x.Id==command.UserId);
            if (user is null) return Result.Failure(UserErrors.NotFound);

            user.UnlockAccount();
            _users.Update(user);
            await _uow.CompleteAsync(ct);

            await _audit.LogAsync(new AuditEntry(
                ActorId: command.ActorId, Action: AuditActions.UserUnlocked,
                EntityType: nameof(User), EntityId: user.Id,
                IpAddress: null, Succeeded: true), ct);

            return Result.Success();
        }
    }

}
