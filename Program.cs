using System.Text.Json.Serialization;
using FluentValidation;
using InventorySystem.Middlewares;
using InventorySystem.Models;
using InventorySystem.Routers;
using InventorySystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

// database
builder.Services.AddDbContextPool<DbInitiate>(ops =>
    ops.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// cors
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(ops =>
{
    ops.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins(allowedOrigins!)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// swagger
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

// validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// configure json request and response
builder.Services.ConfigureHttpJsonOptions(ops =>
{
    ops.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// services
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<SummaryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// middleware
app.UseMiddleware<GlobalException>();
app.UseCors("CorsPolicy");

// routes
app.MapCategoryRoutes();
app.MapProductRoutes();
app.MapStockRoutes();
app.MapSummaryRoutes();

app.Run();
