// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Interfaces;

public sealed class WebhookPayload : Dictionary<string, object>
{
    [JsonPropertyName("repository")]
    public PayloadRepository? Repository { get; set; }

    [JsonPropertyName("issue")]
    public Issue? Issue { get; set; }

    [JsonPropertyName("pull_request")]
    public PullRequest? PullRequest { get; set; }

    [JsonPropertyName("sender")]
    public Sender? Sender { get; set; }

    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("installation")]
    public Installation? Installation { get; set; }

    [JsonPropertyName("comment")]
    public Comment? Comment { get; set; }
}
