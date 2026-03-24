using DesafioFastBackend.Domain.Models;

namespace DesafioFastBackend.Domain.Interfaces.Auth;

public interface IAuthUserRepository
{
    Task<AuthUser?> GetByCredentialsAsync(string username, string password);
}
