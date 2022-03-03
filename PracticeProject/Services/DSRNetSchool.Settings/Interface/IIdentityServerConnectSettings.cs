namespace DSRNetSchool.Settings;

public interface IIdentityServerConnectSettings
{
    string Url { get; }
    string ClientId { get; }
    string ClientSecret { get; }
    bool RequireHttps { get; }
}
