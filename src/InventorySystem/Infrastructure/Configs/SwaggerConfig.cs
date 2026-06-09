using Microsoft.OpenApi;

namespace InventorySystem.Infrastructure.Configs;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(ops =>
        {
            ops.UseInlineDefinitionsForEnums();
            ops.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Inventory System API",
                Version = "v1",
                Description = "API for Inventory System",
                Summary = "API for Inventory System",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
        });
        return services;
    }
}