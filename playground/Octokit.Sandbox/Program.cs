// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using GitHub.Models;
using Actions.Octokit;

var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN")
    ?? throw new InvalidOperationException("The GITHUB_TOKEN environment variable is required.");

var client = GitHubClientFactory.Create(token);
var owner = "dotnet";
var repo = "docs";

var labelName = "profane content 🤬";
var name = Uri.EscapeDataString(labelName);
var label = await GetLabelByNameAsync(name);
if (label is not null)
{

}

/// Tracking a bug:
///   See https://github.com/microsoft/kiota-abstractions-dotnet/pull/205
async Task<Label?> GetLabelByNameAsync(string labelName)
{
    // Actual:      https://api.github.com/repos/dotnet/docs/labels/profane%20content%20%EF%BF%BD%EF%BF%BD
    // Expected:    https://api.github.com/repos/dotnet/docs/labels/profane%20content%20%F0%9F%A4%AC
    // Test:        https://api.github.com/repos/dotnet/docs/labels/profane%20content%20%F0%9F%A4%AC
    //                                                              profane%20content%20%F0%9F%A4%AC

    var request = client.Repos[owner][repo].Labels[Uri.EscapeDataString(labelName)].ToGetRequestInformation();

    return await client.Repos[owner][repo].Labels[labelName].WithUrl(
        $"https://api.github.com/repos/{owner}/{repo}/labels/{labelName}"
        )
        .GetAsync();
}




//var number = 24;

//var pullRequest = await client.Repos[owner][repo].Pulls[number].GetAsync();
//if (pullRequest is not null)
//{
//    var body = new GitHub.Repos.Item.Item.Pulls.Item.WithPull_numberPatchRequestBody
//    {
//        Title = pullRequest.Title,
//        Body = "Test this __\\?\\$\\%\\&\\!\\#\\&__ thing out!",
//    };

//    await client.Repos[owner][repo].Pulls[number].PatchAsync(body);
//}




//var pullRequestNumber = 39864;
//
//var pullRequest = await client.Repos[owner][repo].Pulls[pullRequestNumber].GetAsync();
//if (pullRequest is not null)
//{
//
//}

//var label = await GetLabelByNameAsync1();
//if (label is not null)
//{
//
//}
//
//async Task<Label?> GetLabelByNameAsync1(string labelName = "profane content 🤬")
//{
//    // Actual:      https://api.github.com/repos/dotnet/docs/labels/profane%20content%20%EF%BF%BD%EF%BF%BD
//    // Expected:    https://api.github.com/repos/dotnet/docs/labels/profane%20content%20%F0%9F%A4%AC
//    // Test:        https://api.github.com/repos/dotnet/docs/labels/profane%20content%20%F0%9F%A4%AC
//
//    var request = client.Repos[owner][repo].Labels[Uri.EscapeDataString(labelName)].ToGetRequestInformation();
//
//    return await client.Repos[owner][repo].Labels[labelName].GetAsync();
//}
//
//async Task<Label?> GetLabelByNameAsync(string labelName = "profane content 🤬")
//{
//    Label? label = null;
//
//    const int PageCount = 100;
//    var page = 1;
//
//    while (label is null)
//    {
//        var labels = await client.Repos[owner][repo].Labels.GetAsync(
//            parameters =>
//            {
//                parameters.QueryParameters.Page = page;
//                parameters.QueryParameters.PerPage = PageCount;
//            });
//
//        if (labels is null or { Count: 0 })
//        {
//            break;
//        }
//
//        label = labels?.FirstOrDefault(l => l.Name == labelName);
//        page++;
//    }
//
//    return label;
//}
//
