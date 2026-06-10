using InventorySystem.Features.Users;
using InventorySystem.Shared.Dtos;
using InventorySystem.Shared.Helpers;
using InventorySystem.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Infrastructure.Routers;

public static class UserRouter
{
    public static void MapUserRoutes(this WebApplication app)
    {
        var users = app.MapGroup("/users").WithTags("User");

        // get all users
        users.MapGet("/", async (
            [FromServices] UserService userService
            ) =>
        {
            var users = await userService.GetAllUsers();
            return Results.Ok(new WebResponse<List<UserResponse>>(message: "Users retrieved successfully", data: users));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<List<UserResponse>>>(200)
        .Produces<WebResponse<string>>(500);

        // get user by id
        users.MapGet("/{id}", async (
            [FromServices] UserService userService,
            [FromRoute] long id
            ) =>
        {
            var user = await userService.GetUserById(id);
            return Results.Ok(new WebResponse<UserResponse>(message: "User retrieved successfully", data: user));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<UserResponse>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // add user
        users.MapPost("/", async (
            [FromServices] UserService userService,
            [FromBody] UserAddRequest request
            ) =>
        {
            var user = await userService.AddUser(request);
            return Results.Created($"/users/{user.Id}", new WebResponse<UserResponse>(message: "User created successfully", data: user));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<UserResponse>>(201)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(500);

        // update user
        users.MapPut("/{id}", async (
            [FromServices] UserService userService,
            [FromRoute] long id,
            [FromBody] UserUpdateRequest request
            ) =>
        {
            await userService.UpdateUser(id, request);
            return Results.Ok(new WebResponse<string>(message: "User updated successfully", data: null));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<string>>(200)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // delete user
        users.MapDelete("/{id}", async (
            [FromServices] UserService userService,
            [FromRoute] long id
            ) =>
        {
            await userService.DeleteUser(id);
            return Results.Ok(new WebResponse<string>(message: "User deleted successfully", data: null));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<string>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);
    }
}