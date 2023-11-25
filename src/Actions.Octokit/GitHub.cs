// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit;

public readonly record struct GitHub
{
    private static readonly Lazy<GitHubClient> s_client =
        new(CreateClient);

    public static GitHubClient Client => s_client.Value;

    private static GitHubClient CreateClient()
    {
        var token = GetEnvironmentVariable(GITHUB_TOKEN);

        ArgumentNullException.ThrowIfNullOrWhiteSpace(token);

        return new GitHubClient(
            new ProductHeaderValue(
                "ievangelist-dotnet-github-action-sdk", "1.0"))
        {
            Credentials = new Credentials(token)
        };
    }
}
