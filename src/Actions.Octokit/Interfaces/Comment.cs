// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit.Interfaces;

/// <summary>
/// Represents a comment on a GitHub issue or pull request.
/// </summary>
public sealed class Comment : Dictionary<string, object>
{
    /// <summary>
    /// The unique identifier of the comment.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
}
