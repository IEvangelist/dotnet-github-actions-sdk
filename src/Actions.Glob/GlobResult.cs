// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Glob;

/// <summary>
/// Represents the glob result, indicating whether matches were found and their corresponding <paramref name="Files"/>.
/// </summary>
/// <param name="HasMatches">The value from <see cref="PatternMatchingResult.HasMatches"/>.</param>
/// <param name="Files">The value from <see cref="PatternMatchingResult.Files"/>.</param>
public readonly record struct GlobResult(
    bool HasMatches,
    IEnumerable<FileResult> Files)
{
    /// <summary>
    /// Implicitly converts the <paramref name="result"/> instance to a <see cref="GlobResult"/>.
    /// </summary>
    /// <param name="result">The result instance to convert from.</param>
    public static implicit operator GlobResult(PatternMatchingResult result) =>
        new(
            HasMatches: result.HasMatches,
            Files: result.Files?.Select(match => (FileResult)match)
                ?? Enumerable.Empty<FileResult>());
}