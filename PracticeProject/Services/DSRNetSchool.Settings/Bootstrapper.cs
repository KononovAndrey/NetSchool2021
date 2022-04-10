namespace DSRNetSchool.Settings;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddSingleton<ISettingsSource, SettingsSource>();
        services.AddSingleton<IIdentityServerConnectSettings, IdentityServerConnectSettings>();
        services.AddSingleton<IGeneralSettings, GeneralSettings>();
        services.AddSingleton<IDbSettings, DbSettings>();
        services.AddSingleton<IEmailSettings, EmailSettings>();
        services.AddSingleton<IRabbitMqSettings, RabbitMqSettings>();
        services.AddSingleton<IApiSettings, ApiSettings>();
        services.AddSingleton<IWorkerSettings, WorkerSettings>();
        services.AddSingleton<IIS4Settings, IS4Settings>();

        return services;
    }
}
