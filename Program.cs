using System.Text.Json.Serialization;
using FluentValidation;
using InventorySystem.Middlewares;
using InventorySystem.Models;
using InventorySystem.Routers;
using InventorySystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

// add logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override(
        "Microsoft.AspNetCore",
        LogEventLevel.Warning
    )
    .WriteTo.Console(
    // new RenderedCompactJsonFormatter()
    )
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
// add serilog
builder.Host.UseSerilog();
// add database
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
builder.Services.AddScoped<SummaryService>();

//add cords
builder.Services.AddCors(ops =>
{
    ops.AddPolicy("InventoryUI", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()!)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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
app.MapSummaryRoutes();

// add middleware
app.UseMiddleware<GlobalException>();
app.UseCors("InventoryUI");

app.Run();
