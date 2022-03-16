namespace DSRNetSchool.Api.Test.Services;

using DSRNetSchool.Api.Test.Common;
using DSRNetSchool.API;
using DSRNetSchool.API.Configuration;
using DSRNetSchool.Settings;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

public class TestServices
{
    private readonly IServiceCollection services = new ServiceCollection();
    public IServiceProvider ServiceProvider { get; set; }

    public TestServices()
    {
        var configuration = ConfigurationFactory.GetApiConfiguration();

        // Logger
        var logger = new LoggerConfiguration()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var settings = new ApiSettings(new SettingsSource(configuration));

        services.AddHttpContextAccessor();

        services.AddAppDbContext(settings);

        services.AddAppVersions();

        services.AddAppServices();

        services.AddAutoMappers();

        services.AddControllers().AddValidator();

        ServiceProvider = services.BuildServiceProvider();
    }

    public T Get<T>()
    {
        return ServiceProvider.GetService<T>();
    }


}
