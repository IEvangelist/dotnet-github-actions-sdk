// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Output;

internal sealed class DefaultConsole : IConsole
{
    public void Exit(int exitCode = 0) =>
        Environment.Exit(
            exitCode: Environment.ExitCode = exitCode);

    public void Write(string? message = null) =>
        Console.Write(message);

    public void WriteLine(string? message = null) =>
        Console.WriteLine(message);

    public void WriteError(string message) =>
        Console.Error.Write(message);

    public void WriteErrorLine(string message) =>
        Console.Error.WriteLine(message);
}