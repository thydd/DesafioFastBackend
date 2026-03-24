using DesafioFastBackend.Domain.Interfaces.Auth;
using DesafioFastBackend.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace DesafioFastBackend.Infrastructure.Auth;

public class ConfigurationAuthUserRepository(IConfiguration configuration) : IAuthUserRepository
{
    public Task<AuthUser?> GetByCredentialsAsync(string username, string password)
    {
        var users = configuration
            .GetSection("Auth:Users")
            .GetChildren()
            .Select(x => new AuthUser
            {
                Username = x["Username"] ?? string.Empty,
                Password = x["Password"] ?? string.Empty,
                Role = x["Role"] ?? string.Empty
            });

        var user = users.FirstOrDefault(x =>
            string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase)
            && x.Password == password);

        return Task.FromResult(user);
    }
}
