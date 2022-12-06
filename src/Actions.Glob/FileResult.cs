// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Glob;

/// <summary>
/// Represents a file found by a glob.
/// </summary>
/// <param name="Path">The value from the <see cref="FilePatternMatch.Path"/>.</param>
/// <param name="Stem">The value from the <see cref="FilePatternMatch.Stem"/>.</param>
public readonly record struct FileResult(
    string Path,
    string? Stem)
{
    /// <summary>
    /// Implicitly converts the <paramref name="match"/> instance to a <see cref="FileResult"/>.
    /// </summary>
    /// <param name="match">The match instance to convert from.</param>
    public static implicit operator FileResult(FilePatternMatch match) =>
        new(match.Path, match.Stem);
}