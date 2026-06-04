using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Configs;

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