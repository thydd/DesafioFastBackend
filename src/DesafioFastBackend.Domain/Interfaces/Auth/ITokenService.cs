using DesafioFastBackend.Domain.Models;

namespace DesafioFastBackend.Domain.Interfaces.Auth;

public interface ITokenService
{
    string GenerateToken(AuthUser user);
}
