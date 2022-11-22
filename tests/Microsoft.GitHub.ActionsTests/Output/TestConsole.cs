// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.ActionsTests.Output;

internal sealed class TestConsole : IConsole
{
    public StringBuilder Output { get; } = new();
    public StringBuilder ErrorOutput { get; } = new();

    public int ExitCode = 0;
    public bool Exited = false;
    
    public void Exit(int exitCode = 0) =>
        (ExitCode, Exited) = (exitCode, true);

    public void Write(string? message = null) =>
        Output.Append(message);
            
    public void WriteLine(string? message = null) =>
        Output.AppendLine(message);
           
    public void WriteError(string message) =>
        ErrorOutput.Append(message);
            
    public void WriteErrorLine(string message) =>
        ErrorOutput.AppendLine(message); 
}
