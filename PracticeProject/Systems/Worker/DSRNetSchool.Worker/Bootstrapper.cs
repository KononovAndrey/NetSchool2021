namespace DSRNetSchool.Worker;

using DSRNetSchool.EmailService;
using DSRNetSchool.RabbitMQService;
using DSRNetSchool.Settings;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddSettings()
            .AddEmailSender()
            .AddRabbitMq()
            .AddSingleton<ITaskExecutor, TaskExecutor>();
            ;

        return services;
    }
}
 



