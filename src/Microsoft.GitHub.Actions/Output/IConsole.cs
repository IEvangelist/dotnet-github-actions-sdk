// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Output;

/// <summary>
/// A console interface that abstracts the interactions of the <see cref="Console"/>.
/// </summary>
public interface IConsole
{
    /// <inheritdoc cref="Environment.Exit" />
    void Exit(int exitCode = 0) =>
        Environment.Exit(
            Environment.ExitCode = exitCode);

    /// <inheritdoc cref="Console.Write" />
    void Write(string? message = null) =>
        Console.Write(message);

    /// <inheritdoc cref="Console.WriteLine" />
    void WriteLine(string? message = null) =>
        Console.WriteLine(message);

    /// <inheritdoc cref="TextWriter.Write" />
    void WriteError(string message) =>
        Console.Error.Write(message);

    /// <inheritdoc cref="TextWriter.WriteLine" />
    void WriteErrorLine(string message) =>
        Console.Error.WriteLine(message);
}