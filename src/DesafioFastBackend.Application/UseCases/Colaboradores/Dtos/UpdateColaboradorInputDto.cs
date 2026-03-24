namespace DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;

public sealed record UpdateColaboradorInputDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
}
