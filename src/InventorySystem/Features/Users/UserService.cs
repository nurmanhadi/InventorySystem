using FluentValidation;
using InventorySystem.Infrastructure.Databases;
using InventorySystem.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace InventorySystem.Features.Users;

public class UserService(DbInitiate db, ILogger<UserService> logger, IValidator<UserAddRequest> userAddValidator, IValidator<UserUpdateRequest> userUpdateValidator)
{
    private readonly DbInitiate db = db;
    private readonly ILogger<UserService> logger = logger;
    private readonly IValidator<UserAddRequest> userAddValidator = userAddValidator;
    private readonly IValidator<UserUpdateRequest> userUpdateValidator = userUpdateValidator;

    // get all users
    public async Task<List<UserResponse>> GetAllUsers()
    {
        var users = await db.Users.AsNoTracking()
        .Select(u => new UserResponse
        {
            Id = u.Id,
            Username = u.Username,
            Role = u.Role
        })
        .ToListAsync();
        logger.LogInformation("All users retrieved with count {UserCount}", users.Count);
        return users;
    }

    // get user by id
    public async Task<UserResponse> GetUserById(long id)
    {
        var user = await db.Users.AsNoTracking()
        .Where(u => u.Id == id)
        .Select(u => new UserResponse
        {
            Id = u.Id,
            Username = u.Username,
            Role = u.Role
        })
        .FirstOrDefaultAsync();

        if (user == null)
        {
            logger.LogWarning("User with id {UserId} not found", id);
            throw new NotFoundException("User not found");
        }

        logger.LogInformation("User with id {UserId} retrieved", id);
        return user;
    }

    // add user
    public async Task<UserResponse> AddUser(UserAddRequest request)
    {
        await UserValidation(userAddValidation: request);
        await CheckUsernameExists(request.Username);
        var hashPassword = BC.HashPassword(request.Password);

        var user = new User
        {
            Username = request.Username,
            Password = hashPassword,
            Role = request.Role
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        logger.LogInformation("User with id {UserId} created", user.Id);

        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role
        };
    }

    // update user
    public async Task UpdateUser(long id, UserUpdateRequest request)
    {
        await UserValidation(userUpdateValidation: request);
        var user = await db.Users.FindAsync(id);
        if (user == null)
        {
            logger.LogWarning("User with id {UserId} not found", id);
            throw new NotFoundException("User not found");
        }
        if (!string.IsNullOrEmpty(request.Username))
        {
            await CheckUsernameExists(request.Username);
            user.Username = request.Username;
        }
        if (!string.IsNullOrEmpty(request.Password))
        {
            var hashPassword = BC.HashPassword(request.Password);
            user.Password = hashPassword;
        }
        if (request.Role.HasValue)
        {
            user.Role = request.Role.Value;
        }
        await db.SaveChangesAsync();

        logger.LogInformation("User with id {UserId} updated successfully", user.Id);
    }

    // delete user
    public async Task DeleteUser(long id)
    {
        var user = await db.Users.FindAsync(id);
        if (user == null)
        {
            logger.LogWarning("User with id {UserId} not found", id);
            throw new NotFoundException("User not found");
        }
        db.Users.Remove(user);
        await db.SaveChangesAsync();

        logger.LogInformation("User with id {UserId} deleted successfully", user.Id);
    }

    // validation
    private async Task UserValidation(UserAddRequest? userAddValidation = null, UserUpdateRequest? userUpdateValidation = null)
    {
        if (userAddValidation != null)
        {
            var validationResult = await userAddValidator.ValidateAsync(userAddValidation);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("User validation failed: {Errors}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }

        if (userUpdateValidation != null)
        {
            var validationResult = await userUpdateValidator.ValidateAsync(userUpdateValidation);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("User validation failed: {Errors}", validationResult.Errors.First().ErrorMessage);
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
        }
    }

    // check if username already exists
    private async Task CheckUsernameExists(string username)
    {
        var countUsername = await db.Users.AsNoTracking().CountAsync(u => u.Username == username);
        if (countUsername > 0)
        {
            logger.LogWarning("User with username {Username} already exists", username);
            throw new BadRequestException("Username already exists");
        }
    }
}