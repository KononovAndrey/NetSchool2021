namespace DSRNetSchool.Settings;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddSettings(this IServiceCollection services)
    {
        services.AddSingleton<ISettingsSource, SettingsSource>();
        services.AddSingleton<IApiSettings, ApiSettings>();
        services.AddSingleton<IIdentityServerSettings, IdentityServerSettings>();
        services.AddSingleton<IGeneralSettings, GeneralSettings>();
        services.AddSingleton<IDbSettings, DbSettings>();

        return services;
    }
}
