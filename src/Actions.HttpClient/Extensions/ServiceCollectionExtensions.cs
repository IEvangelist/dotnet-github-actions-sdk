// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Actions.HttpClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient(ClientNames.Basic, client =>
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes("username:password")));
        });

        services.AddHttpClient(ClientNames.Bearer, client =>
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                "token");
        });

        services.AddHttpClient(ClientNames.Pat, client =>
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes("PAT:token")));
        });

        return services;
    }
}
