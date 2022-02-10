namespace DSRNetSchool.API.Configuration;

using Microsoft.AspNetCore.Mvc;

public static class VersioningConfiguration
{
    public static IServiceCollection AddAppVersions(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    //public static WebApplication UseAppVersions(this WebApplication app)
    //{
    //    return app;
    //}
}