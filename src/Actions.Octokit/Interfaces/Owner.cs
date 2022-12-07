﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit.Interfaces;

/// <summary>
/// Represents the owner of a GitHub repository.
/// </summary>
public sealed class Owner : Dictionary<string, object>
{
    /// <summary>
    /// The login used for by the owner.
    /// </summary>
    [JsonPropertyName("login")]
    public required string Login { get; set; }

    /// <summary>
    /// The name of the owner.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
