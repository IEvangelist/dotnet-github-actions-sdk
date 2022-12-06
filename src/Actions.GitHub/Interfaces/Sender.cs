// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.GitHub.Interfaces;

/// <summary>
/// Represents the sender of the GitHub event.
/// </summary>
public sealed class Sender : Dictionary<string, object>
{
    /// <summary>
    /// The type of event that was triggered by the sender.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }
}
