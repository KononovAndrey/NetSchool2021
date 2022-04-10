namespace DSRNetSchool.RabbitMQService;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddSingleton<IRabbitMq, RabbitMq>();
        services.AddSingleton<IRabbitMqTask, RabbitMqTask>();

        return services;
    }
}
