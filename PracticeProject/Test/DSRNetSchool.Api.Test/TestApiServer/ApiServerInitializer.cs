namespace DSRNetSchool.Api.Test.TestApiServer;

using DSRNetSchool.Api.Test.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog;
using System;
using System.Net.Http;

internal class ApiServerInitializer
{

    private IWebHostBuilder builder;
    private HttpClient httpClient;

    public ApiServerInitializer GetWebHost(HttpClient identityServerClient, IConfiguration? configuration = null, Action<IServiceCollection>? configurator = null)
    {
        configuration ??= ConfigurationFactory.GetApiConfiguration();

        // Logger
        var logger = new LoggerConfiguration()
            .Enrich.WithCorrelationId()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        configurator += s =>
        {
            // Common mocks for server
            s.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{identityServerClient.BaseAddress}.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(identityServerClient) { RequireHttps = false, SendAdditionalHeaderData = true }
                    );
            });
        };

        builder = new WebHostBuilder()
            .UseConfiguration(configuration)
            .UseSerilog(logger, true)
            .UseStartup(app => new ApiStartup(configuration, configurator));

        return this;
    }

    public TestServer Build()
    {
        var server = new TestServer(builder) { BaseAddress = new Uri("http://localhost_api/") };
        httpClient = server.CreateClient();
        return server;
    }

    public static TestServer Initialize(HttpClient identityServerClient, IConfiguration? configuration = null, Action<IServiceCollection>? configurator = null)
    {
        return new ApiServerInitializer().GetWebHost(identityServerClient, configuration, configurator).Build();
    }
}
