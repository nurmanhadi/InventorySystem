using InventorySystem.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Infrastructure.Configs;

public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<DbInitiate>(ops =>
            ops.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            )
        );
        return services;
    }
}