namespace DSRNetSchool.Api.Test.Common.Extensions;

using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public static class HttpClientExtensions {
    public static async Task<HttpResponseMessage> SendRequest(
       this HttpClient httpClient,
       HttpMethod method,
       string requestUri,
       JToken? json = null,
       string? jwtToken = null,
       Dictionary<string, string>? headers = null
   )
    {
        var message = new HttpRequestMessage(method, requestUri).AddJsonContent(json).AddJwtToken(jwtToken);
        if (headers != null)
            foreach (var (header, value) in headers)
                message.Headers.Add(header, value);
        return await httpClient.SendAsync(message)!;
    }

    public static async Task<HttpResponseMessage> PostJson(
        this HttpClient httpClient,
        string requestUri,
        JToken? json,
        string? jwtToken = null,
        Dictionary<string, string>? headers = null
    )
    {
        return await httpClient.SendRequest(HttpMethod.Post, requestUri, json, jwtToken, headers);
    }
    public static async Task<HttpResponseMessage> PostJson(
        this HttpClient httpClient,
        string requestUri,
        object request,
        string? jwtToken = null,
        Dictionary<string, string>? headers = null
    )
    {
        return await httpClient.SendRequest(HttpMethod.Post, requestUri, request.AsJObject(), jwtToken, headers);
    }

    public static async Task<HttpResponseMessage> Post(
        this HttpClient httpClient,
        string requestUri,
        string? jwtToken = null
    )
    {
        return await httpClient.PostJson(requestUri, null, jwtToken);
    }

    public static async Task<HttpResponseMessage> PutJson(
        this HttpClient httpClient,
        string requestUri,
        JObject? json,
        string? jwtToken = null
    )
    {
        return await httpClient.SendRequest(HttpMethod.Put, requestUri, json, jwtToken);
    }

    public static async Task<HttpResponseMessage> PutJson(
        this HttpClient httpClient,
        string requestUri,
        object request,
        string? jwtToken = null
    )
    {
        return await httpClient.PutJson(requestUri, request.AsJObject(), jwtToken);
    }

    public static async Task<HttpResponseMessage> Put(
        this HttpClient httpClient,
        string requestUri,
        string? jwtToken = null
    )
    {
        return await httpClient.PutJson(requestUri, null, jwtToken);
    }

    public static async Task<HttpResponseMessage> Get(
        this HttpClient httpClient,
        string requestUri,
        string? jwtToken = null
    )
    {
        return await httpClient.SendRequest(HttpMethod.Get, requestUri, jwtToken: jwtToken);
    }

    public static async Task<HttpResponseMessage> Delete(
        this HttpClient httpClient,
        string requestUri,
        string? jwtToken = null
    )
    {
        return await httpClient.SendRequest(HttpMethod.Delete, requestUri, jwtToken: jwtToken);
    }

    public static async Task<HttpResponseMessage> DeleteJson(
        this HttpClient httpClient,
        string requestUri,
        object request,
        string jwtToken = null
    )
    {
        return await httpClient.SendRequest(HttpMethod.Delete, requestUri, request.AsJObject(), jwtToken);
    }

    private static async Task<string> GetTokenEndpoint(this HttpClient client)
    {
        var disco = await client.GetDiscoveryDocumentAsync(
            new DiscoveryDocumentRequest
            {
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

        return disco.TokenEndpoint;
    }

    public static async Task<TokenResponse> RefreshApiAccessToken(this HttpClient client, string refreshToken, string clientId, string clientSecret)
    {
        var tokenRequest = new RefreshTokenRequest
        {
            ClientSecret = clientSecret,
            ClientId = clientId,
            Scope = "offline_access, api",
            RefreshToken = refreshToken,
            Address = await client.GetTokenEndpoint()
        };

        return await client.RequestRefreshTokenAsync(tokenRequest);
    }

    public static async Task<TokenResponse> GetApiAccessToken(this HttpClient client, string userName, string password, string scope, string clientId, string clientSecret)
    {
        var tokenRequest = new PasswordTokenRequest
        {
            GrantType = "password",
            ClientSecret = clientSecret,
            ClientId = clientId,
            Scope = scope,
            Address = await client.GetTokenEndpoint(),
            Password = password,
            UserName = userName
        };

        return await client.RequestPasswordTokenAsync(tokenRequest);
    }

    public static async Task<(TResponse, HttpStatusCode)> GetResponse<TResponse>(
    this HttpClient client,
    HttpRequestMessage request
)
    {
        var response = await client.SendAsync(request);
        return (await response.Content.ReadAsAsync<TResponse>(), response.StatusCode);
    }
}
