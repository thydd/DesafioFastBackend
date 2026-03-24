using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DesafioFastBackend.Domain.Interfaces.Auth;
using DesafioFastBackend.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DesafioFastBackend.Infrastructure.Auth;

public class JwtTokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateToken(AuthUser user)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var keyValue = configuration["Jwt:Key"];
        var expiresInMinutes = int.TryParse(configuration["Jwt:ExpiresInMinutes"], out var parsedExpiresInMinutes)
            ? parsedExpiresInMinutes
            : 180;

        if (string.IsNullOrWhiteSpace(keyValue))
        {
            throw new InvalidOperationException("Chave JWT não configurada.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
