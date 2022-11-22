// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Output;

/// <inheritdoc cref="IConsole" />
internal sealed class DefaultConsole : IConsole
{
    /// <inheritdoc />
    public void Exit(int exitCode = 0) =>
        Environment.Exit(
            exitCode: Environment.ExitCode = exitCode);

    /// <inheritdoc />
    public void Write(string? message = null) =>
        Console.Write(message);

    /// <inheritdoc />
    public void WriteLine(string? message = null) =>
        Console.WriteLine(message);

    /// <inheritdoc />
    public void WriteError(string message) =>
        Console.Error.Write(message);

    /// <inheritdoc />
    public void WriteErrorLine(string message) =>
        Console.Error.WriteLine(message);
}