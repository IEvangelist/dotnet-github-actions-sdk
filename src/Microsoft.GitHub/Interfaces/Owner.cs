// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Interfaces;

public sealed class Owner : Dictionary<string, object>
{
    [JsonPropertyName("login")]
    public required string Login { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
