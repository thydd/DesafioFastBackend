namespace DesafioFastBackend.Application.UseCases.Workshops.Dtos;

public sealed record UpdateWorkshopInputDto
{
    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string Descricao { get; init; } = string.Empty;
    public DateTime DataRealizacao { get; init; }
}
