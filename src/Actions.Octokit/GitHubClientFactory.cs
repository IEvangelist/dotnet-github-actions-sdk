// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit;

public static class GitHubClientFactory
{
    /// <summary>
    /// Creates a new <see cref="GitHubClient"/> from the given <paramref name="token"/>.
    /// </summary>
    /// <param name="token">The token used to initialize the client.</param>
    /// <returns>A new <see cref="GitHubClient"/> instance.</returns>
    public static GitHubClient Create(string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        return new GitHubClient(
            new ProductHeaderValue(
                "ievangelist-dotnet-github-action-sdk", "1.0"))
        {
            Credentials = new Credentials(token)
        };
    }
}
