namespace DSRNetSchool.Settings;

public interface IApiSettings
{
    IGeneralSettings General { get; }
    IDbSettings Db { get; }
    IIdentityServerSettings IdentityServer { get; }
}

