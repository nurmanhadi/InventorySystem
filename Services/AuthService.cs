using FluentValidation;
using InventorySystem.Dtos;
using InventorySystem.Exceptions;
using InventorySystem.Models;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace InventorySystem.Services;

public class AuthService(DbInitiate db, ILogger<AuthService> logger, IValidator<AuthLoginRequest> authLoginValidator)
{
    private readonly DbInitiate db = db;
    private readonly ILogger<AuthService> logger = logger;
    private readonly IValidator<AuthLoginRequest> authLoginValidator = authLoginValidator;

    // login
    public async Task<UserResponse> Login(AuthLoginRequest request)
    {
        await authValidation(authLoginRequest: request);
        var user = await db.Users
        .AsNoTracking()
        .Where(u => u.Username == request.Username)
        .FirstOrDefaultAsync();

        if (user == null)
        {
            logger.LogWarning("User with username {Username} not found", request.Username);
            throw new BadRequestException("username or password is incorrect");
        }
        if (!BC.Verify(request.Password, user.Password))
        {
            logger.LogWarning("Invalid password for user {Username}", request.Username);
            throw new BadRequestException("username or password is incorrect");
        }

        logger.LogInformation("User {Username} logged in successfully", request.Username);
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role
        };
    }

    // validation
    private async Task AuthValidation(AuthLoginRequest? authLoginRequest = null)
    {
        if (authLoginRequest != null)
        {
            var validationResult = await authLoginValidator.ValidateAsync(authLoginRequest);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Auth login validation failed: {Errors}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}