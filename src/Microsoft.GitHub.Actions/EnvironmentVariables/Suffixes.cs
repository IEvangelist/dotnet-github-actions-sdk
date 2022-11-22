// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.EnvironmentVariables;

/// <summary>
/// A collection of environment variable prefixes, used with corresponding <see cref="Prefixes"/>.
/// </summary>
public static class Suffixes
{
    // Suffixes
    public const string ENV = nameof(ENV);
    public const string STATE = nameof(STATE);
    public const string OUTPUT = nameof(OUTPUT);
}
