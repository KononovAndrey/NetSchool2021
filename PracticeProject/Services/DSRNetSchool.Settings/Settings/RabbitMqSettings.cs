namespace DSRNetSchool.Settings;

public class RabbitMqSettings : IRabbitMqSettings
{
    private readonly ISettingsSource source;

    public RabbitMqSettings(ISettingsSource source)
    {
        this.source = source;
    }

    public string Uri => source.GetAsString("RabbitMQ:Uri");
    public string UserName => source.GetAsString("RabbitMQ:Username");
    public string Password => source.GetAsString("RabbitMQ:Password");
}
