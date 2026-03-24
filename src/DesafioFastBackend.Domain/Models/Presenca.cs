using DesafioFastBackend.Domain.Enums;

namespace DesafioFastBackend.Domain.Models;

public class Presenca
{
    public int WorkshopId { get; set; }
    public int ColaboradorId { get; set; }
    public DateTime DataHoraCheckIn { get; set; }
    public EStatus Status { get; set; }
}
