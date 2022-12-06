// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Core.Commands;

/// <inheritdoc cref="ICommandIssuer" />
internal sealed class DefaultCommandIssuer : ICommandIssuer
{
    private readonly IConsole _console;

    public DefaultCommandIssuer(IConsole console) => _console = console;

    /// <inheritdoc />
    public void Issue<T>(string commandName, T? message = default) =>
        IssueCommand(commandName, null, message);

    /// <inheritdoc />
    public void IssueCommand<T>(
        string commandName,
        IReadOnlyDictionary<string, string>? properties = default,
        T? message = default)
    {
        var cmd = new Command<T>(
            commandName, message, properties);

        if (cmd is not { Conventional: true })
        {
            _console.WriteLine("Issuing unconventional command.");
        }

        var commandMessage = cmd.ToString();
        _console.WriteLine(commandMessage);
    }
}
