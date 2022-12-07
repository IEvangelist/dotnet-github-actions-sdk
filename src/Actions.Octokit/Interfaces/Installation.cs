// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit.Interfaces;

/// <summary>
/// Represents the installation.
/// </summary>
public sealed class Installation : Dictionary<string, object>
{
    /// <summary>
    /// The unique identifier of the installation.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
}
