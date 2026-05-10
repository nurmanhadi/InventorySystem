using System.Text.Json.Serialization;
using FluentValidation;
using InventorySystem.Middlewares;
using InventorySystem.Models;
using InventorySystem.Routers;
using InventorySystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextPool<DbInitiate>(ops =>
    ops.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
// add validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// configure json request and response
builder.Services.ConfigureHttpJsonOptions(ops =>
{
    ops.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ops =>
{
    ops.UseInlineDefinitionsForEnums();
    ops.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inventory System API",
        Version = "v1",
        Description = "API for Inventory System",
        Summary = "API for Inventory System",
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

// add services
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<StockService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add routes
app.MapCategoryRoutes();
app.MapProductRoutes();
app.MapStockRoutes();

// add middleware
app.UseMiddleware<GlobalException>();

app.Run();
