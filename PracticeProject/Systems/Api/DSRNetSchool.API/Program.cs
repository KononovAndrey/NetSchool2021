using DSRNetSchool.API.Configuration;
using DSRNetSchool.Settings;
using Serilog;

// Configure application
var builder = WebApplication.CreateBuilder(args);

// Logger
builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration
    .Enrich.WithCorrelationId()
    .ReadFrom.Configuration(hostBuilderContext.Configuration);
});

var settings = new ApiSettings(new SettingsSource());

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(settings);

services.AddAppHealthCheck();

services.AddAppVersions();

services.AddAppSwagger(settings);

services.AddAppCors();

services.AddControllers();

services.AddRazorPages();

services.AddSettings();


var app = builder.Build();

Log.Information("Starting up");

app.UseStaticFiles();

app.UseRouting();

app.UseAppCors();

app.UseAppHealthCheck();

app.UseSerilogRequestLogging();

app.UseAppSwagger();

app.MapRazorPages();

app.MapControllers();

app.UseAppDbContext();

app.Run();
