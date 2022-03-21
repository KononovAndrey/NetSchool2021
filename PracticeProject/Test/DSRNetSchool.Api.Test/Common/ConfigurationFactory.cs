namespace DSRNetSchool.Api.Test.Common;

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class ConfigurationFactory
{
    private static KeyValuePair<string, string> Val(string key, string value)
    {
        return new KeyValuePair<string, string>(key, value);
    }

    public static IEnumerable<KeyValuePair<string, string>> GetVariables()
    {
        var variables = new List<KeyValuePair<string, string>>
        {
            Val("CONNECTION_STRING_HOST", "localhost,21433"),
            Val("CONNECTION_STRING_DATABASE", "DSRNetSchool_Test"),
            Val("CONNECTION_STRING_USER", "sa"),
            Val("CONNECTION_STRING_PASSWORD", "Passw0rd"),

            Val("IDENTITY_SERVER_URL", "http://localhost_is"),
            Val("IDENTITY_SERVER_CLIENT_ID", "frontend"),
            Val("IDENTITY_SERVER_CLIENT_SECRET", "secret"),
            Val("GENERAL_SWAGGER_VISIBLE", "false")
        }.ToList();

        return variables;
    }

    public static IConfigurationRoot GetApiConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddInMemoryCollection(GetVariables())
            .Build();
    }
}
