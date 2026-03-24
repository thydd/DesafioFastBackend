namespace DesafioFastBackend.Application.UseCases.Auth.Login.Dtos;

public sealed record LoginOutputDto
{
    public string AccessToken { get; init; } = string.Empty;
    public string TokenType { get; init; } = "Bearer";
    public string Username { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}
