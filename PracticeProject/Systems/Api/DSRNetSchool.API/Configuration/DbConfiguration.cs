namespace DSRNetSchool.API.Configuration;

using DSRNetSchool.Db.Context.Context;
using DSRNetSchool.Db.Context.Factories;
using DSRNetSchool.Db.Context.Setup;
using DSRNetSchool.Settings;
using Microsoft.Extensions.DependencyInjection;

public static class DbConfiguration
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IApiSettings settings)
    {
        var dbOptionsDelegate = DbContextOptionFactory.Configure(settings.Db.ConnectionString);

        services.AddDbContextFactory<MainDbContext>(dbOptionsDelegate, ServiceLifetime.Singleton);

        return services;
    }

    public static IApplicationBuilder UseAppDbContext(this IApplicationBuilder app)
    {
        DbInit.Execute(app.ApplicationServices);

        DbSeed.Execute(app.ApplicationServices);

        return app;
    }
}
