namespace DSRNetSchool.Api.Test.TestApiServer;

using DSRNetSchool.Identity;
using DSRNetSchool.Identity.Configuration;
using DSRNetSchool.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

public class IdentityServerStartup
{
    private readonly IConfiguration configuration;
    private readonly Action<IServiceCollection>? configurator;

    public IdentityServerStartup(
        IConfiguration configuration, Action<IServiceCollection>? configurator = null
    )
    {
        this.configuration = configuration;
        this.configurator = configurator;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var settings = new IS4Settings(new SettingsSource(configuration));

        services.AddAppCors();
        services.AddAppDbContext(settings.Db);
        services.AddRouting();
        services.AddHttpContextAccessor();
        services.AddAppServices();
        services.AddIS4();

        configurator?.Invoke(services); // what is it? for what?
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseAppCors();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAppDbContext();
        app.UseIS4();
    }
}

