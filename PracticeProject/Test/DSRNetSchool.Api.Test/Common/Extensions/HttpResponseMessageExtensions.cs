namespace DSRNetSchool.Api.Test.Common.Extensions;

using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public static class HttpResponseMessageExtensions
{
    public static async Task<JObject> ReadAsJson(this HttpResponseMessage httpResponseMessage)
    {
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        return JObject.Parse(content);
    }

    public static async Task<T> ReadAsObject<T>(this HttpResponseMessage httpResponseMessage)
    {
        return await httpResponseMessage.Content.ReadAsAsync<T>();
    }
}
