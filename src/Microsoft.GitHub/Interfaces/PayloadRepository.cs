// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Interfaces;

public sealed class PayloadRepository : Dictionary<string, object>
{
    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("owner")]
    public required Owner Owner { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }
}
