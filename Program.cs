using System.Text.Json.Serialization;
using FluentValidation;
using InventorySystem.Middlewares;
using InventorySystem.Models;
using InventorySystem.Routers;
using InventorySystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;
using Serilog;
using OpenTelemetry.Resources;
using OpenTelemetry.Exporter;
using Npgsql;
using Microsoft.AspNetCore.Authentication.Cookies;

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

// opentelemetry
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing //tracing
        .AddAspNetCoreInstrumentation() // Automatic tracking request HTTP in
        .AddHttpClientInstrumentation() // Automatic tracking request HTTP out
        .AddNpgsql() // Automatic tracking query to database
        .AddOtlpExporter(ops =>
        {
            ops.Endpoint = new Uri(builder.Configuration["Otel:Exporter:Otlp:Endpoint"]!);
            ops.Protocol = OtlpExportProtocol.HttpProtobuf;
        }))
    .WithMetrics(metrics => metrics // metrics
        .AddAspNetCoreInstrumentation() // Automatic tracking request HTTP in
        .AddHttpClientInstrumentation() // Automatic tracking request HTTP out
        .AddRuntimeInstrumentation() // Automatic tracking resources CPU, memory, dll
        .AddOtlpExporter(ops =>
        {
            ops.Endpoint = new Uri(builder.Configuration["Otel:Exporter:Otlp:Endpoint"]!);
            ops.Protocol = OtlpExportProtocol.HttpProtobuf;
        }));

builder.Logging.AddOpenTelemetry(logging => // logging
{
    logging.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Configuration["Otel:ServiceName"]!));
    logging.AddOtlpExporter(ops =>
        {
            ops.Endpoint = new Uri(builder.Configuration["Otel:Exporter:Otlp:Endpoint"]!);
            ops.Protocol = OtlpExportProtocol.HttpProtobuf;
        });
});

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

// cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(ops =>
{
    ops.LoginPath = "/auth/login";
    ops.AccessDeniedPath = "/auth/access-denied";
    ops.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    ops.SlidingExpiration = true;
    ops.Cookie.HttpOnly = true;
    ops.Cookie.SameSite = SameSiteMode.Lax;
    ops.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    ops.Cookie.Name = "InventorySystem.Session";
});

// authorization
builder.Services.AddAuthorization();

// services
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<SummaryService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// middleware
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseSerilogRequestLogging(ops =>
{
    ops.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms | request_id={RequestId}";
    ops.EnrichDiagnosticContext = (diag, http) =>
    {
        diag.Set("RequestId", http.TraceIdentifier);
    };
});
app.UseMiddleware<GlobalException>();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

// routes
app.MapCategoryRoutes();
app.MapProductRoutes();
app.MapStockRoutes();
app.MapSummaryRoutes();
app.MapAuthRoutes();

app.Run();
