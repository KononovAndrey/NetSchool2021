namespace DSRNetSchool.Settings;

public class GeneralSettings : IGeneralSettings
{
    private readonly ISettingsSource source;
    public GeneralSettings(ISettingsSource source) => this.source = source;

    public bool SwaggerVisible => source.GetAsBool("General:SwaggerVisible");
}
