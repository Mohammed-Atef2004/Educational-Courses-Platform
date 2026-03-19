

using EducationalPlatform.Application.Features.Users.Commands.Admin.ChangeRole;
using EducationalPlatform.Application.Features.Users.Commands.Admin.Deactivate;
using EducationalPlatform.Application.Features.Users.Commands.Admin.Delete;
using EducationalPlatform.Application.Features.Users.Commands.Admin.ReActivate;
using EducationalPlatform.Application.Features.Users.Commands.Admin.UnlockAccount;
using EducationalPlatform.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EducationalPlatform.Presentation.Controllers;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin,SuperAdmin")]
public sealed class AdminController : ControllerBase
{
    private readonly ISender _sender;

    public AdminController(ISender sender) => _sender = sender;

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // ─── PUT api/admin/users/{userId}/role ────────────────────────────────────

    [HttpPut("{userId:guid}/role")]
    public async Task<IActionResult> ChangeRole(
        Guid userId,
        [FromBody] ChangeRoleRequest request,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new ChangeRoleCommand(userId, CurrentUserId, request.NewRole), ct);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Error);
    }

    // ─── POST api/admin/users/{userId}/deactivate ─────────────────────────────

    [HttpPost("{userId:guid}/deactivate")]
    public async Task<IActionResult> DeactivateUser(
        Guid userId,
        [FromBody] DeactivateRequest request,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new DeactivateUserCommand(userId, CurrentUserId, request.Reason), ct);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Error);
    }

    // ─── POST api/admin/users/{userId}/reactivate ─────────────────────────────

    [HttpPost("{userId:guid}/reactivate")]
    public async Task<IActionResult> ReactivateUser(Guid userId, CancellationToken ct)
    {
        var result = await _sender.Send(
            new ReactivateUserCommand(userId, CurrentUserId), ct);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Error);
    }

    // ─── DELETE api/admin/users/{userId} ──────────────────────────────────────

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(
        Guid userId,
        [FromBody] DeleteRequest request,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new DeleteUserCommand(userId, CurrentUserId, request.Reason), ct);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Error);
    }

    // ─── POST api/admin/users/{userId}/unlock ─────────────────────────────────

    [HttpPost("{userId:guid}/unlock")]
    public async Task<IActionResult> UnlockAccount(Guid userId, CancellationToken ct)
    {
        var result = await _sender.Send(
            new UnlockAccountCommand(userId, CurrentUserId), ct);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Error);
    }
}

// ─── Request DTOs ─────────────────────────────────────────────────────────────

public sealed record ChangeRoleRequest(UserRole NewRole);
public sealed record DeactivateRequest(string Reason);
public sealed record DeleteRequest(string Reason);