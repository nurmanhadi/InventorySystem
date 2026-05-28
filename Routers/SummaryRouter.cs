using InventorySystem.Helpers;
using InventorySystem.Services;
using Microsoft.AspNetCore.Mvc;

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
        .Produces<WebResponse<SummaryResponse>>(200)
        .Produces<WebResponse<string>>(500);
    }
}