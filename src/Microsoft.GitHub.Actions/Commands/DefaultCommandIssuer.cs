// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Commands;

internal sealed class DefaultCommandIssuer : ICommandIssuer
{
    private readonly IConsole _console;

    public DefaultCommandIssuer(IConsole console) => _console = console;

    /// <summary>
    /// Issues a <see cref="WorkflowCommand"/> with the following format:
    /// <c>::name key=value,key=value::message</c>
    /// </summary>
    /// <example>
    /// <list type="bullet">
    /// <item><c>::warning::This is the message</c></item>
    /// <item><c>::set-env name=MY_VAR::some value</c></item>
    /// </list>
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <param name="command">One of the valid values from <see cref="CommandConstants"/></param>
    /// <param name="properties">An optional set of command properties</param>
    /// <param name="message">An optional command message</param>
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
