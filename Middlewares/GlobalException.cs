using InventorySystem.Exceptions;
using InventorySystem.Helpers;

namespace InventorySystem.Middlewares;

public class GlobalException(RequestDelegate next, ILogger<GlobalException> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalException> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            if (!context.Response.HasStarted)
            {
                if (context.Response.StatusCode == 401)
                {
                    _logger.LogWarning("Unauthorized access attempt.");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new WebResponse<string>(
                        message: "You need to login first.",
                        data: null
                    ));
                }
                else if (context.Response.StatusCode == 403)
                {
                    _logger.LogWarning("Forbidden access attempt.");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new WebResponse<string>(
                        message: "You do not have permission to access this resource.",
                        data: null
                    ));
                }
            }
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, 404, ex.Message);
        }
        catch (BadRequestException ex)
        {
            await HandleExceptionAsync(context, 400, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            await HandleExceptionAsync(context, 500, "An unexpected error occurred on the server.");
        }
    }

    // Helper method agar kode Anda lebih bersih dan tidak DRY (Don't Repeat Yourself)
    private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        // Sesuaikan dengan format WebResponse<T> yang Anda pakai di Router kemarin
        await context.Response.WriteAsJsonAsync(new WebResponse<string>(
            message: message,
            data: null
        ));
    }
}