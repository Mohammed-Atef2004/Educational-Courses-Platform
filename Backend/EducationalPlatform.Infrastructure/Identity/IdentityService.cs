using Domain.Interfaces.Services;
using EducationalPlatform.Domain.Users;
using EducationalPlatform.Domain.Users.Errors;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<string>> CreateUserAsync(string email, string password, CancellationToken ct = default)
    {
        var normalizedEmail = email.ToLowerInvariant();

        var user = new ApplicationUser
        {
            UserName = normalizedEmail,
            Email = normalizedEmail
        };

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded
            ? Result<string>.Success(user.Id)
            : Result<string>.Failure(MapIdentityErrors(result.Errors));
    }

    public async Task<Result> DeleteUserAsync(string identityId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var result = await _userManager.DeleteAsync(user);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(MapIdentityErrors(result.Errors));
    }

    public async Task<Result> AssignRoleAsync(string identityId, UserRole role, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var roleName = role.ToString();

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
                return Result.Failure(MapIdentityErrors(roleResult.Errors));
        }

        if (await _userManager.IsInRoleAsync(user, roleName))
            return Result.Success();

        var result = await _userManager.AddToRoleAsync(user, roleName);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(MapIdentityErrors(result.Errors));
    }

    public async Task<Result> RemoveRoleAsync(string identityId, UserRole role, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var roleName = role.ToString();

        if (!await _userManager.IsInRoleAsync(user, roleName))
            return Result.Success();

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(MapIdentityErrors(result.Errors));
    }

    public async Task<bool> CheckPasswordAsync(string identityId, string password, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        return user is not null && await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<Result> ChangePasswordAsync(string identityId, string currentPassword, string newPassword, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(MapIdentityErrors(result.Errors));
    }

    public async Task<Result> ResetPasswordAsync(string identityId, string token, string newPassword, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(MapIdentityErrors(result.Errors));
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email, CancellationToken ct = default)
    {
        var user = await _userManager.FindByEmailAsync(email)
            ?? throw new InvalidOperationException($"Identity user with email {email} not found.");

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool> IsEmailConfirmedAsync(string identityId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        return user is not null && await _userManager.IsEmailConfirmedAsync(user);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(string identityId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId)
            ?? throw new InvalidOperationException($"Identity user {identityId} not found.");

        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<Result> ConfirmEmailAsync(string identityId, string token, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var result = await _userManager.ConfirmEmailAsync(user, token);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(UserErrors.SecurityErrors.InvalidEmailToken);
    }

    public async Task<string> GenerateChangeEmailTokenAsync(string identityId, string newEmail, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId)
            ?? throw new InvalidOperationException($"Identity user {identityId} not found.");

        return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
    }

    public async Task<Result> ConfirmEmailChangeAsync(string identityId, string newEmail, string token, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user is null)
            return Result.Failure(UserErrors.NotFound);

        var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

        if (!result.Succeeded)
            return Result.Failure(UserErrors.SecurityErrors.InvalidEmailToken);

        user.UserName = newEmail.ToLowerInvariant();
        await _userManager.UpdateAsync(user);

        return Result.Success();
    }

    private static Error MapIdentityErrors(IEnumerable<IdentityError> errors)
    {
        var combined = string.Join(" | ", errors.Select(e => e.Description));

        return new Error(
            "Identity.Error",
            string.IsNullOrWhiteSpace(combined) ? "An identity error occurred." : combined);
    }
}
