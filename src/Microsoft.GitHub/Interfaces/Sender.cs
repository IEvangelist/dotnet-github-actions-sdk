// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Interfaces;

public sealed class Sender : Dictionary<string, object>
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
}
