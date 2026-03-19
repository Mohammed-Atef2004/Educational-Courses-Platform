using Domain.Users;
using EducationalPlatform.Domain.Users;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IIdentityService
    {

        Task<Result<string>> CreateUserAsync(string email, string password, CancellationToken ct = default);

        Task<Result> DeleteUserAsync(string identityId, CancellationToken ct = default);


        Task<Result> AssignRoleAsync(string identityId, UserRole role, CancellationToken ct = default);
        Task<Result> RemoveRoleAsync(string identityId, UserRole role, CancellationToken ct = default);


        Task<bool> CheckPasswordAsync(string identityId, string password, CancellationToken ct = default);
        Task<Result> ChangePasswordAsync(string identityId, string currentPassword, string newPassword, CancellationToken ct = default);
        Task<Result> ResetPasswordAsync(string identityId, string token, string newPassword, CancellationToken ct = default);

        Task<string> GeneratePasswordResetTokenAsync(string email, CancellationToken ct = default);


        Task<bool> IsEmailConfirmedAsync(string identityId, CancellationToken ct = default);
        Task<string> GenerateEmailConfirmationTokenAsync(string identityId, CancellationToken ct = default);
        Task<Result> ConfirmEmailAsync(string identityId, string token, CancellationToken ct = default);


        Task<string> GenerateChangeEmailTokenAsync(string identityId, string newEmail, CancellationToken ct = default);
        Task<Result> ConfirmEmailChangeAsync(string identityId, string newEmail, string token, CancellationToken ct = default);
    }
}
