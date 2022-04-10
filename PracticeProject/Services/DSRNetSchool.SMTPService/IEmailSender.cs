namespace DSRNetSchool.EmailService;

using DSRNetSchool.RabbitMQService;

public interface IEmailSender
{
    Task SendEmailAsync(EmailModel model);
    Task SendEmailAsync(string email, string subject, string message);
}
