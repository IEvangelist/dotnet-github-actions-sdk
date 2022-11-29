// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.EnvironmentVariables;

/// <summary>
/// A collection of environment variable prefixes, used with corresponding <see cref="Suffixes"/>.
/// </summary>
public static class Prefixes
{
    // Prefixes
    public const string GITHUB_ = nameof(GITHUB_);
    public const string INPUT_ = nameof(INPUT_);
    public const string STATE_ = nameof(STATE_);
}
