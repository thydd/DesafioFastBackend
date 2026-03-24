using DesafioFastBackend.Domain.Models;

namespace DesafioFastBackend.Domain.Interfaces.Repositories;

public interface IWorkshopRepository
{
    Task<Workshop> CreateAsync(Workshop workshop);
    Task<IEnumerable<Workshop>> GetAllAsync();
    Task<Workshop?> GetByIdAsync(int id);
    Task<Workshop?> UpdateAsync(Workshop workshop);
    Task<bool> DeleteAsync(int id);
}
