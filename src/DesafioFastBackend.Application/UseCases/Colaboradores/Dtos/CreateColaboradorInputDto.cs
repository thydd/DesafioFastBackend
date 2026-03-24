namespace DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;

public sealed record CreateColaboradorInputDto
{
    public string Nome { get; init; } = string.Empty;
}
