// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Octokit services to the <see cref="IServiceCollection"/>.
    /// <list type="bullet">
    /// <item><see cref="GitHub.Client"/>: A <see cref="GitHubClient"/> instance initialized with the <c>GITHUB_TOKEN</c>.</item>
    /// <item><see cref="Context.Current"/>: The current context as materialized from the workflows environment.</item>
    /// </list>
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The same service collection, but with added services.</returns>
    public static IServiceCollection AddOctokitServices(
        this IServiceCollection services)
    {
        services.AddSingleton(GitHub.Client);

        services.AddSingleton(Context.Current);

        return services;
    }
}
