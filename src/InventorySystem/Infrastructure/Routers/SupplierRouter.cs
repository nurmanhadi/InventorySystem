using InventorySystem.Features.Suppliers;
using InventorySystem.Shared.Dtos;
using InventorySystem.Shared.Helpers;
using InventorySystem.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Infrastructure.Routers;

public static class SupplierRouter
{
    public static void MapSupplierRoutes(this WebApplication app)
    {
        var suppliers = app.MapGroup("/suppliers").WithTags("Supplier");

        // get suppliers
        suppliers.MapGet("/", async (
            [FromServices] SupplierService supplierService,
            [FromQuery(Name = "page")] int page = 1,
            [FromQuery(Name = "size")] int size = 10,
            [FromQuery(Name = "order-by")] OrderBy? orderBy = OrderBy.Ascending,
            [FromQuery(Name = "supplier-active")] SupplierActive? supplierActive = null,
            [FromQuery(Name = "supplier-order-by")] SupplierOrderBy? supplierOrderBy = null
        ) =>
        {
            var response = await supplierService.GetSuppliers(page, size, orderBy, supplierActive, supplierOrderBy);
            return Results.Ok(new WebResponse<WebPaginationResponse<SupplierResponse>>(message: "suppliers retireved successfully", data: response));
        })
        .Produces<WebResponse<WebPaginationResponse<SupplierResponse>>>(200)
        .Produces<WebResponse<string>>(500);

        // get supplier
        suppliers.MapGet("/{id}", async (
            [FromServices] SupplierService supplierService,
            [FromRoute] long id
        ) =>
        {
            var response = await supplierService.GetSupplier(id);
            return Results.Ok(new WebResponse<SupplierResponse>(message: "supplier retireved successfully", data: response));
        })
        .Produces<WebResponse<SupplierResponse>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // add supplier
        suppliers.MapPost("/", async (
            [FromServices] SupplierService supplierService,
            [FromBody] SupplierAddRequest supplierAddRequest
        ) =>
        {
            var response = await supplierService.AddSupplier(supplierAddRequest);
            return Results.Created(
                $"/suppliers/{response.Id}",
                new WebResponse<SupplierResponse>(message: "supplier created successfully", data: response)
            );
        })
        .Produces<WebResponse<SupplierResponse>>(201)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(500);

        // update supplier
        suppliers.MapPut("/{id}", async (
            [FromServices] SupplierService supplierService,
            [FromRoute] long id,
            [FromBody] SupplierUpdateRequest supplierUpdateRequest
        ) =>
        {
            await supplierService.UpdateSupplier(id, supplierUpdateRequest);
            return Results.Ok(new WebResponse<string>(message: "supplier update successfully"));
        })
        .Produces<WebResponse<string>>(200)
        .Produces<WebResponse<string>>(400)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);

        // delete supplier
        suppliers.MapDelete("/{id}", async (
            [FromServices] SupplierService supplierService,
            [FromRoute] long id
        ) =>
        {
            await supplierService.DeleteSupplier(id);
            return Results.Ok(new WebResponse<string>(message: "supplier delete successfully"));
        })
        .Produces<WebResponse<string>>(200)
        .Produces<WebResponse<string>>(404)
        .Produces<WebResponse<string>>(500);
    }
}