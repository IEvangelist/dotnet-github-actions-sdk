// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Commands;

/// <inheritdoc cref="ICommandIssuer" />
internal sealed class DefaultCommandIssuer : ICommandIssuer
{
    private readonly IConsole _console;

    public DefaultCommandIssuer(IConsole console) => _console = console;

    public void IssueCommand<T>(
        string command,
        IDictionary<string, string>? properties = default,
        T? message = default)
    {
        var cmd = new Command<T>(
            command, message, properties);

        if (cmd is { Conventional: false })
        {
            _console.WriteLine("Issuing unconventional command.");
        }
        
        _console.WriteLine(cmd.ToString());
    }

    public void Issue<T>(string name, T? message = default) =>
        IssueCommand(name, null, message);
}
