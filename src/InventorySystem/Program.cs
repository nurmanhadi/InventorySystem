using System.Text.Json.Serialization;
using FluentValidation;
using InventorySystem.Infrastructure.Configs;
using InventorySystem.Infrastructure.Middlewares;
using InventorySystem.Infrastructure.Routers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

// config
builder.Services
.AddDatabaseConfig(builder.Configuration)
.AddOpenTelemetryConfig(builder.Configuration, builder.Logging)
.AddCorsConfig(builder.Configuration)
.AddSwaggerConfig()
.AddAuthenticationConfig()
.AddAuthorizationConfig()
.AddApplicationServices();


// configure json request and response
builder.Services.ConfigureHttpJsonOptions(ops =>
{
    ops.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


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
app.MapAuthRoutes();
app.MapUserRoutes();
app.MapSupplierRoutes();

app.Run();
