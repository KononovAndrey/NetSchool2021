using DSRNetSchool.Settings;
using DSRNetSchool.Worker;
using Serilog;

// Configure application
var builder = WebApplication.CreateBuilder(args);

// Settings for the initial configuration
var settings = new WorkerSettings(new SettingsSource());

// Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(logger, true);

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppHealthCheck();
services.RegisterServices();


// Start application

var app = builder.Build();

app.UseAppHealthCheck();

app.StartTaskExecutor();

app.Run();

