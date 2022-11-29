// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Interfaces;

public sealed class Installation : Dictionary<string, object>
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
}
