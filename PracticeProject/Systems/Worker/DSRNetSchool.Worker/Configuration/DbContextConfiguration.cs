namespace DSRNetSchool.Worker;

using DSRNetSchool.Db.Context.Context;
using DSRNetSchool.Db.Context.Factories;
using DSRNetSchool.Db.Context.Setup;
using DSRNetSchool.Settings;

public static class DbContextConfiguration
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IWorkerSettings settings)
    {
        var dbOptionsDelegate = DbContextOptionFactory.Configure(settings.Db.ConnectionString);

        services.AddDbContextFactory<MainDbContext>(dbOptionsDelegate, ServiceLifetime.Singleton);

        return services;
    }

    public static WebApplication UseAppDbContext(this WebApplication app)
    {
        return app;
    }
}