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

        var authenticationProvider = new TokenAuthenticationProvider(
            "GitHub.Actions.Octokit", token);

        var request = RequestAdapter.Create(
            authenticationProvider);

        return new GitHubClient(request);
    }
}
