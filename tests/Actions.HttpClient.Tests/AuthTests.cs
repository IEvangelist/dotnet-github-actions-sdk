// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient.Tests;

public class AuthTests
{
    [Fact]
    public async Task HttpGetRequestWithBasicAuth_CorrectlyDeserializesTypedResponse()
    {
        using var client = new ServiceCollection()
            .AddHttpClientServices()
            .BuildServiceProvider()
            .GetRequiredService<IHttpCredentialClientFactory>()
            .CreateBasicClient("johndoe", "password");

        var response = await client.GetAsync(
            "https://postman-echo.com/get",
            SourceGenerationContext.Default.PostmanEchoGetResponse);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Result);
        var auth = response.Result.Headers["authorization"];
        var creds = auth["Basic ".Length..].FromBase64();
        Assert.Equal("johndoe:password", creds);
        Assert.Equal("https://postman-echo.com/get", response.Result.Url);
    }

    [Fact]
    public async Task HttpGetRequestWithBearerAuth_CorrectlyDeserializesTypedResponse()
    {
        var token = "scbfb44vxzku5l4xgc3qfazn3lpk4awflfryc76esaiq7aypcbhs";

        using var client = new ServiceCollection()
            .AddHttpClientServices()
            .BuildServiceProvider()
            .GetRequiredService<IHttpCredentialClientFactory>()
            .CreateBearerTokenClient(token);

        var response = await client.GetAsync(
            "https://postman-echo.com/get",
            SourceGenerationContext.Default.PostmanEchoGetResponse);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Result);
        var auth = response.Result.Headers["authorization"];
        Assert.Equal($"Bearer {token}", auth);
        Assert.Equal("https://postman-echo.com/get", response.Result.Url);
    }

    [Fact]
    public async Task HttpGetRequestWithPatAuth_CorrectlyDeserializesTypedResponse()
    {
        var pat = "scbfb44vxzku5l4xgc3qfazn3lpk4awflfryc76esaiq7aypcbhs";

        using var client = new ServiceCollection()
            .AddHttpClientServices()
            .BuildServiceProvider()
            .GetRequiredService<IHttpCredentialClientFactory>()
            .CreatePersonalAccessTokenClient(pat);

        var response = await client.GetAsync(
            "https://postman-echo.com/get",
            SourceGenerationContext.Default.PostmanEchoGetResponse);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Result);
        var auth = response.Result.Headers["authorization"];
        var creds = auth["Basic ".Length..].FromBase64();
        Assert.Equal($"PAT:{pat}", creds);
        Assert.Equal("https://postman-echo.com/get", response.Result.Url);
    }
}
