// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Glob;

/// <summary>
/// Extensions for registering services with the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all the services required to interact with Actions.Glob services.
    /// Consumers should require the <see cref="IGlobPatternResolverBuilder"/>
    /// to build an <see cref="IGlobPatternResolver"/>.
    /// </summary>
    public static IServiceCollection AddGitHubActionsGlob(this IServiceCollection services)
    {
        services.AddTransient<IGlobPatternResolverBuilder, DefaultGlobPatternResolverBuilder>();

        return services;
    }
}
