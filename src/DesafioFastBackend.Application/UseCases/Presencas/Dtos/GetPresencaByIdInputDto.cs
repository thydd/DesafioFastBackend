namespace DesafioFastBackend.Application.UseCases.Presencas.Dtos;

public sealed record GetPresencaByIdInputDto
{
    public int WorkshopId { get; init; }
    public int ColaboradorId { get; init; }
}
