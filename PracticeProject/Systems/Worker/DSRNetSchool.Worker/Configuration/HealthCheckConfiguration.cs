namespace DSRNetSchool.Worker;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks();

        return services;
    }

    public static void UseAppHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks("/health");
    }
}