using DesafioFastBackend.Domain.Models;

namespace DesafioFastBackend.Domain.Interfaces.Repositories;

public interface IPresencaRepository
{
    Task<Presenca> CreateAsync(Presenca presenca);
    Task<IEnumerable<Presenca>> GetAllAsync();
    Task<Presenca?> GetByIdAsync(int workshopId, int colaboradorId);
    Task<Presenca?> UpdateAsync(Presenca presenca);
    Task<bool> DeleteAsync(int workshopId, int colaboradorId);
}
