﻿namespace DSRNetSchool.Settings;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static void AddSettings(this IServiceCollection services)
    {
        services.AddSingleton<ISettingsSource, SettingsSource>();
        services.AddSingleton<IApiSettings, ApiSettings>();
        services.AddSingleton<IIdentityServerSettings, IdentityServerSettings>();
    }
}
