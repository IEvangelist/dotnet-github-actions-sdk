// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Output;

/// <summary>
/// A console interface that 
/// </summary>
public interface IConsole
{
    void Exit(int exitCode = 0);

    void Write(string? message = null);

    void WriteLine(string? message = null);

    void WriteError(string message);

    void WriteErrorLine(string message);
}
