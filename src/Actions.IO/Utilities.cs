// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;

namespace Actions.IO;

public static class Utilities
{
    public static bool IsWindows { get; } =
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static Lazy<string[]> Paths { get; } = new (() =>
    {
        var path = Environment.GetEnvironmentVariable("PATH");
        return path?.Split(Path.PathSeparator) ?? Array.Empty<string>();
    });

    public static Lazy<string[]> PathExtensions { get; } = new(() =>
    {
        var pathExts = Environment.GetEnvironmentVariable("PATHEXT");
        return pathExts?.Split(Path.PathSeparator) ?? Array.Empty<string>();
    });

    ///<inheritdoc cref="File.Exists(string?)" />
    public static bool Exists(string? path) =>
        File.Exists(path);

    ///<inheritdoc cref="Path.IsPathRooted(string?)" />
    public static bool IsRooted(string? path) =>
        Path.IsPathRooted(path);
    
    public static bool IsDirectory(
        [NotNullWhen(true)] string? path) =>
        Directory.Exists(path) && (File.GetAttributes(path!) & FileAttributes.Directory) is FileAttributes.Directory;

    /// <summary>
    /// Best effort attempt to determine whether a file exists and is executable.
    /// </summary>
    /// <param name="filePath">The file path to check</param>
    /// <param name="extensions">Additional file extensions to try</param>
    /// <returns>If file exists and is executable, returns the file path.
    /// Otherwise empty string</returns>
    public static string TryGetExecutablePath(string filePath, string[] extensions)
    {
        if (Exists(filePath))
        {
            return filePath;
        }

        var dirPath = Path.GetDirectoryName(filePath);
        if (dirPath is null) return string.Empty;

        var fileName = Path.GetFileNameWithoutExtension(filePath);
        foreach (var ext in extensions)
        {
            var fullPath = Path.Combine(dirPath, $"{fileName}{ext}");
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }

        return string.Empty;
    }

    private static bool IsUnixExecutable(FileInfo file) =>
        file.Exists && (file.Attributes & FileAttributes.Archive) is FileAttributes.Archive;
}
