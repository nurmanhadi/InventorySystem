using InventorySystem.Services;

namespace InventorySystem.Configs;

public static class ServiceConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryService>();
        services.AddScoped<ProductService>();
        services.AddScoped<StockService>();
        services.AddScoped<SummaryService>();
        services.AddScoped<AuthService>();
        services.AddScoped<UserService>();
        return services;
    }
}