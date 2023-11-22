// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit;

public sealed class GitHub
{
    private static readonly Lazy<GitHubClient> s_client =
        new(CreateClient);

    public static GitHubClient Client => s_client.Value;

    private static GitHubClient CreateClient()
    {
        var client = new GitHubClient(
            new ProductHeaderValue(
                "ievangelist-dotnet-github-action-sdk"));

        var token = GetEnvironmentVariable(GITHUB_TOKEN);

        if (!string.IsNullOrWhiteSpace(token))
        {
            client.Credentials = new Credentials(token);
        }

        return client;
    }
}
