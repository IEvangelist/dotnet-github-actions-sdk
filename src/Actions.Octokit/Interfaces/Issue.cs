﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit.Interfaces;

/// <summary>
/// Represents a GitHub issue.
/// </summary>
public class Issue : Dictionary<string, object>
{
    /// <summary>
    /// The unique identifier of the issue.
    /// </summary>
    [JsonPropertyName("number")]
    public long Number { get; set; }

    /// <summary>
    /// The URL for the issues HTML.
    /// </summary>
    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }

    /// <summary>
    /// The body text of the issue.
    /// </summary>
    [JsonPropertyName("body")]
    public string? Body { get; set; }
}
