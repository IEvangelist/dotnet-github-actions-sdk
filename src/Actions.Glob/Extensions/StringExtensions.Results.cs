// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Glob;

public static partial class StringExtensions
{
    /// <summary>
    /// Gets the <see cref="GlobResult"/> for the specified directory path and patterns.
    /// </summary>
    /// <param name="directory">The directory path to search for files.
    /// If not provided, defaults to <see cref="Directory.GetCurrentDirectory"/>.</param>
    /// <param name="includePatterns">The include patterns to use when searching for files.</param>
    /// <param name="excludePatterns">The exclude patterns to use when searching for files.</param>
    /// <returns>A <see cref="GlobResult"/> instance representing the results of the glob operation.</returns>
    public static GlobResult GetGlobResult(
        this string? directory,
        IEnumerable<string> includePatterns,
        IEnumerable<string>? excludePatterns = null)
    {
        directory ??= Directory.GetCurrentDirectory();

        Matcher matcher = new();
        matcher.AddIncludePatterns(includePatterns);
        if (excludePatterns is not null)
        {
            matcher.AddExcludePatterns(excludePatterns);
        }

        return matcher.Execute(
            new DirectoryInfoWrapper(
                new DirectoryInfo(directory)));
    }

    /// <summary>
    /// Gets the <see cref="GlobResult"/> for the specified directory path and patterns.
    /// </summary>
    /// <param name="directory">The directory path to search for files.
    /// If not provided, defaults to <see cref="Directory.GetCurrentDirectory"/>.</param>
    /// <param name="includePatterns">The include patterns to use when searching for files.</param>
    /// <returns>A <see cref="GlobResult"/> instance representing the results of the glob operation.</returns>
    public static GlobResult GetGlobResult(
        this string? directory,
        params string[] includePatterns)
    {
        directory ??= Directory.GetCurrentDirectory();

        Matcher matcher = new();
        matcher.AddIncludePatterns(includePatterns);

        return matcher.Execute(
            new DirectoryInfoWrapper(
                new DirectoryInfo(directory)));
    }
}
