namespace DSRNetSchool.RabbitMQService;

using System.Threading.Tasks;

public class RabbitMqTask : IRabbitMqTask
{
    private readonly IRabbitMq rabbitMq;

    public RabbitMqTask(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task SendEmail(EmailModel email)
    {
        await rabbitMq.PushAsync(RabbitMqTaskQueueNames.SEND_EMAIL, email);
    }
}
