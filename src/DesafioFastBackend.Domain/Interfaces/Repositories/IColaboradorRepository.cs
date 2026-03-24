using DesafioFastBackend.Domain.Models;

namespace DesafioFastBackend.Domain.Interfaces.Repositories;

public interface IColaboradorRepository
{
    Task<Colaborador> CreateAsync(Colaborador colaborador);
    Task<IEnumerable<Colaborador>> GetAllAsync();
    Task<Colaborador?> GetByIdAsync(int id);
    Task<Colaborador?> UpdateAsync(Colaborador colaborador);
    Task<bool> DeleteAsync(int id);
}
