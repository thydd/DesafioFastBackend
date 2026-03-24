using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DesafioFastBackend.Domain.Interfaces.Auth;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Infrastructure.Auth;
using DesafioFastBackend.Infrastructure.Repositories;

namespace DesafioFastBackend.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DesafioFastBackendDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
        services.AddScoped<IWorkshopRepository, WorkshopRepository>();
        services.AddScoped<IPresencaRepository, PresencaRepository>();
        services.AddScoped<IAuthUserRepository, ConfigurationAuthUserRepository>();
        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    } 
}
