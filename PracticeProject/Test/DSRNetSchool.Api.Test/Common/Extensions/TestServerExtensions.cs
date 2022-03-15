namespace DSRNetSchool.Api.Test.Common.Extensions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

public static class TestServerExtensions
{
    public static T ResolveService<T>(this TestServer testServer)
    {
        return testServer.Services.GetService<T>();
    }
}
