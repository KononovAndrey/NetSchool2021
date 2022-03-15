namespace DSRNetSchool.Api.Test.TestApiServer;
using DSRNetSchool.API;
using DSRNetSchool.API.Configuration;
using DSRNetSchool.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Serilog;
using System;

public class ApiStartup
{
    private readonly IConfiguration configuration;
    private readonly Action<IServiceCollection>? configurator;

    public ApiStartup(
        IConfiguration configuration, Action<IServiceCollection>? configurator = null
    )
    {
        this.configuration = configuration;
        this.configurator = configurator;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Logger
        var logger = new LoggerConfiguration()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var settings = new ApiSettings(new SettingsSource(configuration));

        services.AddHttpContextAccessor();

        services.AddAppDbContext(settings);

        services.AddAppHealthCheck();

        services.AddAppVersions();

        services.AddAppSwagger(settings);

        services.AddAppCors();

        services.AddAppServices();

        services.AddAppAuth(settings);

        services.AddControllers().AddValidator();

        services.AddRazorPages();

        services.AddAutoMappers();

        IdentityModelEventSource.ShowPII = true;

        configurator?.Invoke(services); // what is it? for what?
    }

    public void Configure(IApplicationBuilder app)
    {
        Log.Information("Starting up");

        app.UseAppMiddlewares();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAppCors();

        app.UseSerilogRequestLogging();

        app.UseAppAuth();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseAppDbContext();
    }
}

