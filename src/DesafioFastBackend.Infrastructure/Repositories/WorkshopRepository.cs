using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioFastBackend.Infrastructure.Repositories;

public class WorkshopRepository(DesafioFastBackendDbContext context) : IWorkshopRepository
{
    public async Task<Workshop> CreateAsync(Workshop workshop)
    {
        context.Workshops.Add(workshop);
        await context.SaveChangesAsync();
        return workshop;
    }

    public async Task<IEnumerable<Workshop>> GetAllAsync()
    {
        return await context.Workshops
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Workshop?> GetByIdAsync(int id)
    {
        return await context.Workshops
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Workshop?> UpdateAsync(Workshop workshop)
    {
        var entity = await context.Workshops.FirstOrDefaultAsync(x => x.Id == workshop.Id);
        if (entity is null)
        {
            return null;
        }

        entity.Nome = workshop.Nome;
        entity.Descricao = workshop.Descricao;
        entity.DataRealizacao = workshop.DataRealizacao;

        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Workshops.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return false;
        }

        context.Workshops.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
