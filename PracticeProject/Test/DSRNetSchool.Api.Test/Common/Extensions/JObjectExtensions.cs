namespace DSRNetSchool.Api.Test.Common.Extensions;

using Newtonsoft.Json.Linq;
using System.Linq;

public static class JObjectExtensions
{
    public static JObject AsJObject(this object obj)
    {
        return JObject.FromObject(obj);
    }

    public static JArray AsJArray<T>(this T[] array)
    {
        return JArray.FromObject(array);
    }

    public static JObject Put(this JObject jObject, string propertyName, JToken newValue)
    {
        jObject.SelectToken(propertyName)?.Replace(newValue);
        return jObject;
    }

    public static JArray PutFirst(this JArray jArray, string propertyName, JToken newValue)
    {
        jArray.FirstOrDefault()?.SelectToken(propertyName)?.Replace(newValue);
        return jArray;
    }

    public static JObject Delete(this JObject jObject, string propertyName)
    {
        jObject.Property(propertyName)?.Remove();
        return jObject;
    }

    public static JObject AddNew(this JObject jObject, string newPropertyName, string newValue)
    {
        jObject.Add(new JProperty(newPropertyName, newValue));
        return jObject;
    }

    public static JObject AddNewAfter(
        this JObject jObject, string existProperty, string newPropertyName,
        string newValue
    )
    {
        jObject.Property(existProperty)?.AddAfterSelf(new JProperty(newPropertyName, newValue));
        return jObject;
    }

    public static string Get(this JToken jToken, string property)
    {
        return jToken.SelectToken(property)?.Value<string>() ?? string.Empty;
    }

    public static T GetAs<T>(this JToken jToken, string property)
    {
        return jToken.SelectToken(property).Value<T>();
    }
}
