namespace DSRNetSchool.Settings;

public class ApiSettings : IApiSettings
{
    private readonly ISettingsSource source;
    private readonly IIdentityServerSettings identityServerSettings;
    private readonly IGeneralSettings generalSettings;
    private readonly IDbSettings dbSettings;

    public ApiSettings(ISettingsSource source) => this.source = source;

    public ApiSettings(ISettingsSource source, IIdentityServerSettings identityServerSettings, IGeneralSettings generalSettings, IDbSettings dbSettings)
    {
        this.source = source;
        this.identityServerSettings = identityServerSettings;
        this.generalSettings = generalSettings;
        this.dbSettings = dbSettings;
    }

    public IIdentityServerSettings IdentityServer => identityServerSettings ?? new IdentityServerSettings(source);

    public IGeneralSettings General => generalSettings ?? new GeneralSettings(source);

    public IDbSettings Db => dbSettings ?? new DbSettings(source);
}
