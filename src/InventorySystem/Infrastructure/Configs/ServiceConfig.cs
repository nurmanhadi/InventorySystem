using InventorySystem.Features.Auth;
using InventorySystem.Features.Categories;
using InventorySystem.Features.Products;
using InventorySystem.Features.Users;

namespace InventorySystem.Infrastructure.Configs;

public static class ServiceConfig
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryService>();
        services.AddScoped<ProductService>();
        services.AddScoped<AuthService>();
        services.AddScoped<UserService>();
        return services;
    }
}