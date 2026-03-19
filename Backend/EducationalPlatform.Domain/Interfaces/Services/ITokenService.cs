using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(TokenClaims claims);

        //Task<string> GenerateRefreshTokenAsync(Guid userId, CancellationToken ct = default);

        
        //Task<TokenPair?> RotateRefreshTokenAsync(string refreshToken, CancellationToken ct = default);

        //Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken ct = default);

        //Task RevokeAllRefreshTokensAsync(Guid userId, CancellationToken ct = default);
    }

    public sealed record TokenClaims(
        Guid UserId,
        string Email,
        string Username,
        string Role);

    public sealed record TokenPair(
        string AccessToken,
        string RefreshToken,
        DateTime AccessTokenExpiresAt,
        DateTime RefreshTokenExpiresAt);

}
