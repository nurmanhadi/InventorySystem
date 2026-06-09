using InventorySystem.Shared.Helpers;

namespace InventorySystem.Infrastructure.Configs;

public static class AuthorizationConfig
{
    public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
        .AddPolicy(RolePolicy.WarehouseOperations.ToString(), policy =>
        {
            policy.RequireRole(UserRole.Admin.ToString(), UserRole.Staff.ToString());
        })
        .AddPolicy(RolePolicy.AdminOnly.ToString(), policy =>
        {
            policy.RequireRole(UserRole.Admin.ToString());
        })
        .AddPolicy(RolePolicy.StaffOnly.ToString(), policy =>
        {
            policy.RequireRole(UserRole.Staff.ToString());
        });
        return services;
    }
}