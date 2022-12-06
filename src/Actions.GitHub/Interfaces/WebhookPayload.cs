// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.GitHub.Interfaces;

/// <summary>
/// The payload of a GitHub webhook.
/// </summary>
public sealed class WebhookPayload : Dictionary<string, object>
{
    /// <summary>
    /// The payload repository.
    /// </summary>
    [JsonPropertyName("repository")]
    public PayloadRepository? Repository { get; set; }

    /// <summary>
    /// The issue from the webhook payload.
    /// </summary>
    [JsonPropertyName("issue")]
    public Issue? Issue { get; set; }

    /// <summary>
    /// The pull request from the webhook payload.
    /// </summary>
    [JsonPropertyName("pull_request")]
    public PullRequest? PullRequest { get; set; }

    /// <summary>
    /// The sender of the webhook payload.
    /// </summary>
    [JsonPropertyName("sender")]
    public Sender? Sender { get; set; }

    /// <summary>
    /// The action from the webhook payload.
    /// </summary>
    [JsonPropertyName("action")]
    public string? Action { get; set; }

    /// <summary>
    /// The installation from the webhook payload.
    /// </summary>
    [JsonPropertyName("installation")]
    public Installation? Installation { get; set; }

    /// <summary>
    /// The comment from the webhook payload.
    /// </summary>
    [JsonPropertyName("comment")]
    public Comment? Comment { get; set; }
}
