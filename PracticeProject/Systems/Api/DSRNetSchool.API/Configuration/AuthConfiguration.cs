namespace DSRNetSchool.API.Configuration;

using DSRNetSchool.Common.Security;
using DSRNetSchool.Db.Context.Context;
using DSRNetSchool.Db.Entities;
using DSRNetSchool.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

public static class AuthConfiguration
{
    public static IServiceCollection AddAppAuth(this IServiceCollection services, IApiSettings settings)
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

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = settings.IdentityServer.RequireHttps;
                options.Authority = settings.IdentityServer.Url;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = "api";
            });


        services.AddAuthorization(options => {
            options.AddPolicy(AppScopes.BooksRead, policy => policy.RequireClaim("scope", AppScopes.BooksRead));
            options.AddPolicy(AppScopes.BooksWrite, policy => policy.RequireClaim("scope", AppScopes.BooksWrite));
        });

        return services;
    }

    public static WebApplication UseAppAuth(this WebApplication app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}