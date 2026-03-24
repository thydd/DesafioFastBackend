using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioFastBackend.Infrastructure.Repositories;

public class ColaboradorRepository(DesafioFastBackendDbContext context) : IColaboradorRepository
{
    public async Task<Colaborador> CreateAsync(Colaborador colaborador)
    {
        context.Colaboradores.Add(colaborador);
        await context.SaveChangesAsync();
        return colaborador;
    }

    public async Task<IEnumerable<Colaborador>> GetAllAsync()
    {
        return await context.Colaboradores
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Colaborador?> GetByIdAsync(int id)
    {
        return await context.Colaboradores
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Colaborador?> UpdateAsync(Colaborador colaborador)
    {
        var entity = await context.Colaboradores.FirstOrDefaultAsync(x => x.Id == colaborador.Id);
        if (entity is null)
        {
            return null;
        }

        entity.Nome = colaborador.Nome;
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Colaboradores.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return false;
        }

        context.Colaboradores.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
