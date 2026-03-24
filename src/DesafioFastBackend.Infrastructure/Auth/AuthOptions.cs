using DesafioFastBackend.Domain.Models;

namespace DesafioFastBackend.Infrastructure.Auth;

public sealed class AuthOptions
{
    public IList<AuthUser> Users { get; init; } = [];
}
