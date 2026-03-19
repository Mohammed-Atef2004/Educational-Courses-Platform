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

namespace EducationalPlatform.Application.Features.Users.Commands.Security.Enable2FA
{
    public sealed class Enable2FACommandHandler
    : IRequestHandler<Enable2FACommand, Result<Enable2FAResponse>>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _uow;
        private readonly ITotpService _totp;
        private readonly IAuditService _audit;

        public Enable2FACommandHandler(
            IUserRepository users,
            IUnitOfWork uow,
            ITotpService totp,
            IAuditService audit)
        {
            _users = users;
            _uow = uow;
            _totp = totp;
            _audit = audit;
        }

        public async Task<Result<Enable2FAResponse>> Handle(Enable2FACommand command, CancellationToken ct)
        {
            var user = _users.EntityQuery.FirstOrDefault(x => x.Id == command.UserId);
            if (user is null)
                return Result<Enable2FAResponse>.Failure(UserErrors.NotFound);

            var secret = _totp.GenerateSecret();
            var qrCodeUri = _totp.GenerateQrCodeUri(user.Email.Value, secret);

            if (!_totp.Verify(secret, command.TotpCode))
                return Result<Enable2FAResponse>.Failure(UserErrors.SecurityErrors.InvalidTotpCode);

            var result = user.EnableTwoFactor(secret);
            if (result.IsFailure)
                return Result<Enable2FAResponse>.Failure(result.Error);

            _users.Update(user);
            await _uow.CompleteAsync(ct);

            await _audit.LogAsync(new AuditEntry(
                ActorId: user.Id, Action: AuditActions.User2FAEnabled,
                EntityType: nameof(User), EntityId: user.Id,
                IpAddress: null, Succeeded: true), ct);

            return Result<Enable2FAResponse>.Success(new Enable2FAResponse(secret, qrCodeUri));
        }
    }
}
