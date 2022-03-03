namespace DSRNetSchool.Identity.Configuration;

using DSRNetSchool.Db.Context.Context;
using DSRNetSchool.Db.Entities;
using DSRNetSchool.Identity.Configuration.IS4;
using Microsoft.AspNetCore.Identity;

public static class IS4Configuration
{
    public static IServiceCollection AddIS4(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services
            .AddIdentityServer()
            .AddAspNetIdentity<User>()
            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
            .AddInMemoryClients(AppClients.Clients)
            .AddInMemoryApiResources(AppResources.Resources)
            .AddInMemoryIdentityResources(AppIdentityResources.Resources)
            .AddDeveloperSigningCredential();

        return services;
    }

    public static WebApplication UseIS4(this WebApplication app)
    {
        app.UseIdentityServer();

        return app;
    }
}
