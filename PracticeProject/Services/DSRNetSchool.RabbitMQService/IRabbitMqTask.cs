namespace DSRNetSchool.RabbitMQService;

using System.Threading.Tasks;

public interface IRabbitMqTask
{
    Task SendEmail(EmailModel email);
}
