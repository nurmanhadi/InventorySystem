using InventorySystem.Dtos;
using InventorySystem.Helpers;
using InventorySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Routers;

public static class UserRouter
{
    public static void MapUserRoutes(this WebApplication app)
    {
        var users = app.MapGroup("/users").WithTags("User");

        // get all users
        users.MapGet("/users", async (
            [FromServices] UserService userService
            ) =>
        {
            var users = await userService.GetAllUsers();
            return Results.Ok(new WebResponse<List<UserResponse>>(message: "Users retrieved successfully", data: users));
        })
        .Produces<WebResponse<List<UserResponse>>>(200)
        .Produces<WebResponse<string>>(500);

        // get user by id
        users.MapGet("/users/{id}", async (
            [FromServices] UserService userService,
            [FromRoute] long id
            ) =>
        {
            var user = await userService.GetUserById(id);
            return Results.Ok(new WebResponse<UserResponse>(message: "User retrieved successfully", data: user));
        })
        .Produces<WebResponse<UserResponse>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // add user
        users.MapPost("/users", async (
            [FromServices] UserService userService,
            [FromBody] UserAddRequest request
            ) =>
        {
            var user = await userService.AddUser(request);
            return Results.Created($"/users/{user.Id}", new WebResponse<UserResponse>(message: "User created successfully", data: user));
        })
        .Produces<WebResponse<UserResponse>>(201)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(500);

        // update user
        users.MapPut("/users/{id}", async (
            [FromServices] UserService userService,
            [FromRoute] long id,
            [FromBody] UserUpdateRequest request
            ) =>
        {
            await userService.UpdateUser(id, request);
            return Results.Ok(new WebResponse<string>(message: "User updated successfully", data: null));
        })
        .Produces<WebResponse<string>>(200)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // delete user
        users.MapDelete("/users/{id}", async (
            [FromServices] UserService userService,
            [FromRoute] long id
            ) =>
        {
            await userService.DeleteUser(id);
            return Results.Ok(new WebResponse<string>(message: "User deleted successfully", data: null));
        })
        .Produces<WebResponse<string>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);
    }
}