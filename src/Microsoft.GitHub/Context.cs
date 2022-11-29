// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub;

public sealed class Context
{
    private static readonly Lazy<Context> s_context = new(() => new());

    public static Context Current => s_context.Value;

    public WebhookPayload? Payload { get; }
    public string? EventName { get; }
    public string? Sha { get; }
    public string? Ref { get; }
    public string? Workflow { get; }
    public string? Action { get; }
    public string? Actor { get; }
    public string? Job { get; }
    public long RunNumber { get; }
    public long RunId { get; }
    public string ApiUrl { get; }
    public string ServerUrl { get; }
    public string GraphQlUrl { get; }

    public Repository Repo { get; }
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
