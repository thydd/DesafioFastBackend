namespace DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;

public sealed record ColaboradorOutputDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
}
