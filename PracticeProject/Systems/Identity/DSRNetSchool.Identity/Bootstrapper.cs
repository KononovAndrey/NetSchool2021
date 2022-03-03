namespace DSRNetSchool.Identity;

using DSRNetSchool.Settings;

public static class Bootstrapper
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services
            .AddSettings()
            ;
    }
}