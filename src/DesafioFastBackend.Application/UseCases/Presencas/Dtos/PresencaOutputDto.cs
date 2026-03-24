using DesafioFastBackend.Domain.Enums;

namespace DesafioFastBackend.Application.UseCases.Presencas.Dtos;

public sealed record PresencaOutputDto
{
    public int WorkshopId { get; init; }
    public int ColaboradorId { get; init; }
    public DateTime DataHoraCheckIn { get; init; }
    public EStatus Status { get; init; }
}
