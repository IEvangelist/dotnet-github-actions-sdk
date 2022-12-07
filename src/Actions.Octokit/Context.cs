// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit;

/// <summary>
/// Provides access to the GitHub Actions context.
/// </summary>
public sealed class Context
{
    private static readonly Lazy<Context> s_context = new(() => new());

    /// <summary>
    /// Gets the current GitHub Actions context.
    /// </summary>
    public static Context Current => s_context.Value;

    /// <summary>
    /// Gets webhook payload in context.
    /// </summary>
    public WebhookPayload? Payload { get; }
    /// <summary>
    /// Gets event name in context.
    /// </summary>
    public string? EventName { get; }
    /// <summary>
    /// Gets the SHA in context.
    /// </summary>
    public string? Sha { get; }
    /// <summary>
    /// Gets the REF in context.
    /// </summary>
    public string? Ref { get; }
    /// <summary>
    /// Gets the workflow in context.
    /// </summary>
    public string? Workflow { get; }
    /// <summary>
    /// Gets the action in context.
    /// </summary>
    public string? Action { get; }
    /// <summary>
    /// Gets the actor in context.
    /// </summary>
    public string? Actor { get; }
    /// <summary>
    /// Gets the job in context.
    /// </summary>
    public string? Job { get; }
    /// <summary>
    /// Gets the run number in context.
    /// </summary>
    public long RunNumber { get; }
    /// <summary>
    /// Gets the run id in context.
    /// </summary>
    public long RunId { get; }
    /// <summary>
    /// Gets the API URL in context.
    /// </summary>
    public string ApiUrl { get; }
    /// <summary>
    /// Gets the server URL in context.
    /// </summary>
    public string ServerUrl { get; }
    /// <summary>
    /// Gets the GraphQL URL in context.
    /// </summary>
    public string GraphQlUrl { get; }
    /// <summary>
    /// Gets the repo in context.
    /// </summary>
    public Repository Repo { get; }
    /// <summary>
    /// Gets the issue in context.
    /// </summary>
    public Common.Issue Issue { get; }

    private Context()
    {
        var eventPath = GetEnvironmentVariable(GITHUB_EVENT_PATH);
        if (!string.IsNullOrWhiteSpace(eventPath))
        {
            if (File.Exists(eventPath))
            {
                var json = File.ReadAllText(eventPath, Encoding.UTF8);
                Payload = JsonSerializer.Deserialize<WebhookPayload>(json)!;
            }
            else
            {
                Console.WriteLine($"GITHUB_EVENT_PATH ${eventPath} does not exist");
            }
        }

        EventName = GetEnvironmentVariable(GITHUB_EVENT_NAME);
        Sha = GetEnvironmentVariable(GITHUB_SHA);
        Ref = GetEnvironmentVariable(GITHUB_REF);
        Workflow = GetEnvironmentVariable(GITHUB_WORKFLOW);
        Action = GetEnvironmentVariable(GITHUB_ACTION);
        Actor = GetEnvironmentVariable(GITHUB_ACTOR);
        Job = GetEnvironmentVariable(GITHUB_JOB);
        
        RunNumber = int.TryParse(GetEnvironmentVariable(GITHUB_RUN_NUMBER), out var number) ? number : 10;
        RunId = int.TryParse(GetEnvironmentVariable(GITHUB_RUN_ID), out var id) ? id : 10;
        ApiUrl = GetEnvironmentVariable(GITHUB_API_URL) ?? "https://api.github.com";
        ServerUrl = GetEnvironmentVariable(GITHUB_SERVER_URL) ?? "https://github.com";
        GraphQlUrl = GetEnvironmentVariable(GITHUB_GRAPHQL_URL) ?? "https://api.github.com/graphql";

        if (Payload is { Repository: { } })
        {
            Issue = new(
                Payload.Repository.Owner.Login,
                Payload.Repository.Name,
                Payload.Issue?.Number ??
                    Payload.PullRequest?.Number ??
                        (long.TryParse(Payload["number"].ToString(), out var payloadNumber)
                            ? payloadNumber : 0));
        }

        var repository = GetEnvironmentVariable(GITHUB_REPOSITORY);
        if (!string.IsNullOrWhiteSpace(repository))
        {
            var parts = repository.Split('/');
            if (parts is { Length: 2 })
            {
                Repo = new Repository(parts[0], parts[1]);
            }
        }
        else if (Payload is { Repository: { } })
        {
            Repo = new Repository(
                Payload.Repository.Owner.Login,
                Payload.Repository.Name);
        }
    }
}
