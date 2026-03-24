using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioFastBackend.Infrastructure.Repositories;

public class PresencaRepository(DesafioFastBackendDbContext context) : IPresencaRepository
{
    public async Task<Presenca> CreateAsync(Presenca presenca)
    {
        context.Presencas.Add(presenca);
        await context.SaveChangesAsync();
        return presenca;
    }

    public async Task<IEnumerable<Presenca>> GetAllAsync()
    {
        return await context.Presencas
            .AsNoTracking()
            .OrderBy(x => x.WorkshopId)
            .ThenBy(x => x.ColaboradorId)
            .ToListAsync();
    }

    public async Task<Presenca?> GetByIdAsync(int workshopId, int colaboradorId)
    {
        return await context.Presencas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.WorkshopId == workshopId && x.ColaboradorId == colaboradorId);
    }

    public async Task<Presenca?> UpdateAsync(Presenca presenca)
    {
        var entity = await context.Presencas
            .FirstOrDefaultAsync(x => x.WorkshopId == presenca.WorkshopId && x.ColaboradorId == presenca.ColaboradorId);

        if (entity is null)
        {
            return null;
        }

        entity.DataHoraCheckIn = presenca.DataHoraCheckIn;
        entity.Status = presenca.Status;

        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int workshopId, int colaboradorId)
    {
        var entity = await context.Presencas
            .FirstOrDefaultAsync(x => x.WorkshopId == workshopId && x.ColaboradorId == colaboradorId);

        if (entity is null)
        {
            return false;
        }

        context.Presencas.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
