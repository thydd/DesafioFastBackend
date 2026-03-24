using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DesafioFastBackend.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DesafioFastBackendDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        return services;
    } 
}
