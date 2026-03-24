namespace DesafioFastBackend.Application.UseCases.Auth.Login.Dtos;

public sealed record LoginInputDto
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
