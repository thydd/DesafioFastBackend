using DesafioFastBackend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioFastBackend.Infrastructure;

public class DesafioFastBackendDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<Presenca> Presencas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Colaborador>(x => x.ToTable("Colaborador"));
        modelBuilder.Entity<Colaborador>().HasKey(x => x.Id);
        modelBuilder.Entity<Colaborador>().Property(x => x.Nome).HasMaxLength(200).IsRequired();

        modelBuilder.Entity<Workshop>(x => x.ToTable("Workshop"));
        modelBuilder.Entity<Workshop>().HasKey(x => x.Id);
        modelBuilder.Entity<Workshop>().Property(x => x.Descricao).HasMaxLength(2000).IsRequired();
        modelBuilder.Entity<Workshop>().Property(x => x.DataRealizacao).IsRequired();

        modelBuilder.Entity<Presenca>(x => x.ToTable("Presenca"));
        modelBuilder.Entity<Presenca>().HasOne<Workshop>().WithMany().HasForeignKey(x => x.WorkshopId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Presenca>().HasOne<Colaborador>().WithMany().HasForeignKey(x => x.ColaboradorId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Presenca>().HasKey(x => new { x.WorkshopId, x.ColaboradorId });
        modelBuilder.Entity<Presenca>().Property(x => x.DataHoraCheckIn).IsRequired();
        modelBuilder.Entity<Presenca>().Property(x => x.Status).IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}
