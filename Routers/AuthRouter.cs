using System.Security.Claims;
using InventorySystem.Dtos;
using InventorySystem.Helpers;
using InventorySystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Routers;

public static class AuthRouter
{
    public static void MapAuthRoutes(this WebApplication app)
    {
        var auth = app.MapGroup("/auth").WithTags("Authentication");

        // login
        auth.MapPost("/login", async (
            HttpContext context,
            [FromServices] AuthService authService,
            [FromBody] AuthLoginRequest request
            ) =>
        {
            var response = await authService.Login(request);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, response.Id.ToString()),
                new(ClaimTypes.Name, response.Username),
                new(ClaimTypes.Role, response.Role.ToString())
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60) // 1 hours
                }
                );
            return Results.Ok(new WebResponse<UserResponse>(message: "Login successful", data: response));
        })
        .Produces<WebResponse<UserResponse>>(200)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(500);
    }
}