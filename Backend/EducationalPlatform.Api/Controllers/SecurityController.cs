
using EducationalPlatform.Application.Features.Users.Commands.Security.Disable2FA;
using EducationalPlatform.Application.Features.Users.Commands.Security.Enable2FA;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EducationalPlatform.Presentation.Controllers;

[ApiController]
[Route("api/security")]
[Authorize]
public sealed class SecurityController : ControllerBase
{
    private readonly ISender _sender;

    public SecurityController(ISender sender) => _sender = sender;

    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // ─── POST api/security/2fa/enable ─────────────────────────────────────────

    [HttpPost("2fa/enable")]
    public async Task<IActionResult> Enable2FA(
        [FromBody] Enable2FARequest request,
        CancellationToken ct)
    {
        var result = await _sender.Send(
            new Enable2FACommand(CurrentUserId, request.TotpCode), ct);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    // ─── POST api/security/2fa/disable ────────────────────────────────────────

    [HttpPost("2fa/disable")]
    public async Task<IActionResult> Disable2FA(CancellationToken ct)
    {
        var result = await _sender.Send(
            new Disable2FACommand(CurrentUserId), ct);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Error);
    }
}

// ─── Request DTOs ─────────────────────────────────────────────────────────────

public sealed record Enable2FARequest(string TotpCode);