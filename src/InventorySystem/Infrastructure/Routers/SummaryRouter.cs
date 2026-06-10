using InventorySystem.Features.Reports;
using InventorySystem.Shared.Dtos;
using InventorySystem.Shared.Helpers;
using InventorySystem.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Infrastructure.Routers;


public static class SummaryRouter
{
    public static void MapSummaryRoutes(this WebApplication app)
    {
        var summary = app.MapGroup("/summary").WithTags("Summary");

        // get summary
        summary.MapGet("/", async (
            [FromServices] SummaryService summaryService
        ) =>
        {
            var response = await summaryService.GetSummary();
            return Results.Ok(new WebResponse<SummaryResponse>(message: "Summary retrieved successfully", data: response));
        })
        .RequireAuthorization(RolePolicy.AdminOnly.ToString())
        .Produces<WebResponse<SummaryResponse>>(200)
        .Produces<WebResponse<string>>(500);
    }
}