
using Domain.Interfaces.Services;
using Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EducationalPlatform.Infrastructure.Services.Token;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; init; } = default!;
    public string Audience { get; init; } = default!;
    public string SecretKey { get; init; } = default!;
    public int AccessTokenMinutes { get; init; } = 15;
    public int RefreshTokenDays { get; init; } = 7;
}

public sealed class TokenService : ITokenService
{
    private readonly JwtOptions _options;
    private readonly AppDbContext _db;

    public TokenService(IOptions<JwtOptions> options, AppDbContext db)
    {
        _options = options.Value;
        _db = db;
    }

    public string GenerateAccessToken(TokenClaims claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtClaims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,   claims.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, claims.Email),
            new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
            new Claim("username",                    claims.Username),
            new Claim(ClaimTypes.Role,               claims.Role),
        };

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: jwtClaims,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //public async Task<string> GenerateRefreshTokenAsync(Guid userId, CancellationToken ct = default)
    //{
    //    var tokenValue = GenerateSecureToken();
    //    var refreshToken = RefreshToken.Create(userId, tokenValue);

    //    await _db.RefreshTokens.AddAsync(refreshToken, ct);
    //    await _db.SaveChangesAsync(ct);

    //    return tokenValue;
    //}

    //public async Task<TokenPair?> RotateRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
    //{
    //    var stored = await _db.RefreshTokens
    //        .FirstOrDefaultAsync(r => r.Token == refreshToken, ct);

    //    if (stored is null || stored.IsExpired) return null;

    //    // Reuse detection
    //    if (stored.IsRevoked)
    //    {
    //        await RevokeAllRefreshTokensAsync(stored.UserId, ct);
    //        return null;
    //    }

    //    var newTokenValue = GenerateSecureToken();
    //    var newRefreshToken = RefreshToken.Create(stored.UserId, newTokenValue);

    //    stored.Revoke(newTokenValue);
    //    await _db.RefreshTokens.AddAsync(newRefreshToken, ct);
    //    await _db.SaveChangesAsync(ct);

    //    var user = await _db.Users.FindAsync(new object[] { stored.UserId }, ct);
    //    if (user is null) return null;

    //    var claims = new TokenClaims(
    //        UserId: user.Id,
    //        Email: user.Email.Value,
    //        Username: user.Username.Value,
    //        Role: user.Role.ToString());

    //    return new TokenPair(
    //        AccessToken: GenerateAccessToken(claims),
    //        RefreshToken: newTokenValue,
    //        AccessTokenExpiresAt: DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
    //        RefreshTokenExpiresAt: newRefreshToken.ExpiresAt);
    //}

    //public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
    //{
    //    var stored = await _db.RefreshTokens
    //        .FirstOrDefaultAsync(r => r.Token == refreshToken, ct);

    //    if (stored is null || stored.IsRevoked) return;

    //    stored.Revoke();
    //    await _db.SaveChangesAsync(ct);
    //}

    //public async Task RevokeAllRefreshTokensAsync(Guid userId, CancellationToken ct = default)
    //{
    //    var tokens = await _db.RefreshTokens
    //        .Where(r => r.UserId == userId && !r.IsRevoked)
    //        .ToListAsync(ct);

    //    foreach (var token in tokens)
    //        token.Revoke();

    //    await _db.SaveChangesAsync(ct);
    //}

    private static string GenerateSecureToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
}