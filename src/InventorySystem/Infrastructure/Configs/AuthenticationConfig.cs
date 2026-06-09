using Microsoft.AspNetCore.Authentication.Cookies;

namespace InventorySystem.Infrastructure.Configs;

public static class AuthenticationConfig
{
    public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(ops =>
        {
            // ops.LoginPath = "/auth/login";
            // ops.AccessDeniedPath = "/auth/access-denied";
            ops.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            ops.SlidingExpiration = true;
            ops.Cookie.HttpOnly = true;
            ops.Cookie.SameSite = SameSiteMode.Lax;
            ops.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            ops.Cookie.Name = "InventorySystem.Session";

            ops.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }
            };
        });
        return services;
    }
}