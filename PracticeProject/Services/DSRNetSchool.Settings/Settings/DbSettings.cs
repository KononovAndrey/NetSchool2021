namespace DSRNetSchool.Settings;

public class DbSettings : IDbSettings
{
    private readonly ISettingsSource source;
    public DbSettings(ISettingsSource source) => this.source = source;

    public string ConnectionString => source.GetConnectionString("MainDbContext");
}
