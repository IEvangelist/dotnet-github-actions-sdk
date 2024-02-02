// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit.Tests;

public class GitHubClientTests
{
    [Fact]
    public async Task GitHubClientGetsFirstPullRequestTest()
    {
        var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

        if (token is null)
        {
            return; // Skip the test if the token isn't available.
        }

        var client = GitHubClientFactory.Create(token);

        var owner = "IEvangelist";
        var repo = "dotnet-github-actions-sdk";
        var pullNumber = 1;

        var firstPullRequest = await client.Repos[owner][repo].Pulls[pullNumber].GetAsync();

        Assert.NotNull(firstPullRequest);
    }
}
