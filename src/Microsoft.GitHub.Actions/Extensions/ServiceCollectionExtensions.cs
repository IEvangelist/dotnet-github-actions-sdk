// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGitHubActions(this IServiceCollection services)
    {
        services.AddSingleton<IConsole, DefaultConsole>();
        services.AddTransient<ICommandIssuer, DefaultCommandIssuer>();
        services.AddTransient<ICommandIssuer, DefaultCommandIssuer>();
        services.AddTransient<IFileCommandIssuer, DefaultFileCommandIssuer>();
        services.AddTransient<IWorkflowStepService, DefaultWorkflowStepService>();

        return services;
    }
}
