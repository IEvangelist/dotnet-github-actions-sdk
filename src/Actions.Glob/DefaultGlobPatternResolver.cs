// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Glob;

/// <inheritdoc cref="IGlobPatternResolver" />
internal sealed class DefaultGlobPatternResolver : IGlobPatternResolver
{
    private readonly IEnumerable<string> _includePatterns = [];
    private readonly IEnumerable<string> _excludePatterns = [];

    private DefaultGlobPatternResolver(
        IEnumerable<string> includePatterns,
        IEnumerable<string> excludePatterns) =>
        (_includePatterns, _excludePatterns) = (includePatterns, excludePatterns);

    /// <inheritdoc />
    internal static IGlobPatternResolver Factory(
        IEnumerable<string> includePatterns,
        IEnumerable<string> excludePatterns) =>
        new DefaultGlobPatternResolver(includePatterns, excludePatterns);

    /// <inheritdoc />
    IEnumerable<string> IGlobPatternResolver.GetGlobFiles(
        string? directory) =>
        directory.GetGlobFiles(
            _includePatterns,
            _excludePatterns);

    /// <inheritdoc />
    GlobResult IGlobPatternResolver.GetGlobResult(
        string? directory) =>
        directory.GetGlobResult(
            _includePatterns,
            _excludePatterns);
}
