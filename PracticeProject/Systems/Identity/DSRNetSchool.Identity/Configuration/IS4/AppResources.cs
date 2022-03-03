namespace DSRNetSchool.Identity.Configuration.IS4;

using Duende.IdentityServer.Models;

public static class AppResources
{
    public static IEnumerable<ApiResource> Resources => new List<ApiResource>
    {
        new ApiResource("api")
    };
}
