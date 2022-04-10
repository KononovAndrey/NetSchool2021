namespace DSRNetSchool.Settings;

public interface IEmailSettings
{
    string FromName { get; }
    string FromEmail { get; }
    string Server { get; }
    int Port { get; }
    string Login { get; }
    string Password { get; }
    bool Ssl { get; }
}
