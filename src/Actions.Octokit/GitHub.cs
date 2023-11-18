// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit;

public sealed class GitHub
{
    private readonly Lazy<GitHubClient> _client;

    public GitHub() => _client = new Lazy<GitHubClient>(CreateClient);

    public GitHubClient Client => _client.Value;

    private static GitHubClient CreateClient()
    {
        var token = GetEnvironmentVariable(GITHUB_TOKEN);
        var client = new GitHubClient(
            new ProductHeaderValue("ievangelist"));

        if (!string.IsNullOrWhiteSpace(token))
        {
            client.Credentials = new Credentials(token);
        }

        return client;
    }
}
