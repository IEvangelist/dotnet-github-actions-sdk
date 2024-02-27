// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient.Extensions;

public static class ServiceCollectionExtensions
{
    public const string UserAgentHeader = "dotnet-github-actions-sdk";

    /// <summary>
    /// Adds the required HTTP client services to the <see cref="IServiceCollection"/>.
    /// Exposes the <see cref="IHttpCredentialClientFactory"/> for consuming services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="userAgent">The optional user-argent value which will be applied
    /// as an HTTP header on all outgoing requests.</param>
    /// <returns></returns>
    public static IServiceCollection AddHttpClientServices(
        this IServiceCollection services,
        string? userAgent = UserAgentHeader)
    {
        services.AddSingleton<IHttpCredentialClientFactory, DefaultHttpKeyedClientFactory>();

        services.AddHttpClient(ClientNames.Basic, ConfigureClient)
                .AddStandardResilienceHandler();

        services.AddHttpClient(ClientNames.Bearer, ConfigureClient)
                .AddStandardResilienceHandler();

        services.AddHttpClient(ClientNames.Pat, ConfigureClient)
                .AddStandardResilienceHandler();

        return services;

        void ConfigureClient(NetClient client)
        {
            if (userAgent is not null)
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            }

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
