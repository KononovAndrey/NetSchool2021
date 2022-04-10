namespace DSRNetSchool.API;

using DSRNetSchool.AuthorService;
using DSRNetSchool.BookService;
using DSRNetSchool.EmailService;
using DSRNetSchool.RabbitMQService;
using DSRNetSchool.Settings;
using DSRNetSchool.UserAccount;

public static class Bootstrapper
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services
            .AddSettings()
            .AddAuthorService()
            .AddBookService()
            .AddEmailSender()
            .AddRabbitMq()
            .AddUserAccountService()
            ;
    }
}