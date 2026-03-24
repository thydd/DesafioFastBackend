namespace DesafioFastBackend.Application.UseCases.Presencas.Dtos;

public sealed record DeletePresencaInputDto
{
    public int WorkshopId { get; init; }
    public int ColaboradorId { get; init; }
}
