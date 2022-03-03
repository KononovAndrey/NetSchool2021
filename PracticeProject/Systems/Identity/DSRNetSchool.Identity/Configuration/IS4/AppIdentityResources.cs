namespace DSRNetSchool.Identity.Configuration.IS4;

using Duende.IdentityServer.Models;

public static class AppIdentityResources
{
    public static IEnumerable<IdentityResource> Resources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };
}
