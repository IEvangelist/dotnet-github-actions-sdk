// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Services;

/// <inheritdoc cref="IWorkflowStepService" />
internal sealed class DefaultWorkflowStepService : IWorkflowStepService
{
    private readonly IConsole _console;
    private readonly ICommandIssuer _commandIssuer;
    private readonly IFileCommandIssuer _fileCommandIssuer;

    public DefaultWorkflowStepService(
        IConsole console,
        ICommandIssuer commandIssuer,
        IFileCommandIssuer fileCommandIssuer) =>
        (_console, _commandIssuer, _fileCommandIssuer) =
            (console, commandIssuer, fileCommandIssuer);

    /// <inheritdoc />
    public bool IsDebug => GetEnvironmentVariable(RUNNER_DEBUG) is "1";

    /// <inheritdoc />
    public async ValueTask AddPathAsync(string path)
    {
        var filePath = GetEnvironmentVariable(GITHUB_PATH);
        if (filePath is not null)
        {
            await _fileCommandIssuer.IssueFileCommandAsync(PATH, path);
        }
        else
        {
            _commandIssuer.IssueCommand(CommandConstants.AddPath, null, path);
        }

        SetEnvironmentVariable(
            PATH,
            $"{path}{PathSeparator}{GetEnvironmentVariable(PATH)}");
    }

    /// <inheritdoc />
    public void Debug(string message) =>
        _commandIssuer.IssueCommand(
            CommandConstants.Debug, message: message);

    /// <inheritdoc />
    public void EndGroup() =>
        _commandIssuer.Issue(
            CommandConstants.EndGroup, "");

    /// <inheritdoc />
    public void Error(string message, AnnotationProperties? properties = default) =>
        _commandIssuer.IssueCommand(
            CommandConstants.Error, properties?.ToCommandProperties(), message);

    /// <inheritdoc />
    public void ExportVariable(string name, string value)
    {
        value = value.ToCommandValue();
        SetEnvironmentVariable(name, value);

        var filePath = GetEnvironmentVariable(GITHUB_ENV);
        if (filePath is not null)
        {
            _fileCommandIssuer.IssueFileCommandAsync(
                ENV,
                _fileCommandIssuer.PrepareKeyValueMessage(name, value));
        }
        else
        {
            _commandIssuer.IssueCommand<string>(
                CommandConstants.SetEnv,
                name.ToCommandProperties());
        }
    }

    /// <inheritdoc />
    public bool GetBoolInput(string name, InputOptions? options = default)
    {
        var value = GetInput(name, options);
        if (bool.TryParse(value, out var result))
        {
            return result;
        }

        throw new Exception($"""
            Input does not meet YAML 1.2 "Core Schema" specification: {name}
            Support boolean input list: \`true | True | TRUE | false | False | FALSE\`
            """);
    }

    /// <inheritdoc />
    public string GetInput(string name, InputOptions? options = default)
    {
        var value = GetEnvironmentVariable($"{GITHUB_INPUT_PREFIX}{name.Replace(' ', '_').ToUpperInvariant()}");
        if (options.HasValue && options.Value is { Required: true } &&
            string.IsNullOrWhiteSpace(value))
        {
            throw new Exception(
                $"Input required and not supplied: {name}");
        }

        if (options.HasValue && options.Value is { TrimWhitespace: false })
        {
            return value ?? "";
        }

        return value?.Trim() ?? "";
    }

    /// <inheritdoc />
    public string[] GetMultilineInput(string name, InputOptions? options = default)
    {
        var inputs = GetInput(name, options)
            .Split('\n', StringSplitOptions.RemoveEmptyEntries);

        if (options.HasValue && options.Value is { TrimWhitespace: false })
        {
            return inputs;
        }

        return inputs.Select(input => input.Trim()).ToArray();
    }

    /// <inheritdoc />
    public string GetState(string name) =>
        GetEnvironmentVariable(
            $"{GITHUB_STATE_PREFIX}{name}") ?? "";

    /// <inheritdoc />
    public async Task<T> GroupAsync<T>(string name, Func<Task<T>> task)
    {
        T result;
        try
        {
            StartGroup(name);

            result = await task();
        }
        finally
        {
            EndGroup();
        }

        return result;
    }

    /// <inheritdoc />
    public void Info(string message) => _console.WriteLine(message);

    /// <inheritdoc />
    public void Notice(string message, AnnotationProperties? properties = default) =>
        _commandIssuer.IssueCommand(
            CommandConstants.Notice, properties?.ToCommandProperties(), message);

    /// <inheritdoc />
    public async Task SaveStateAsync(string name, string value)
    {
        var filePath = GetEnvironmentVariable(GITHUB_STATE);
        if (filePath is not null)
        {
            await _fileCommandIssuer.IssueFileCommandAsync(
                STATE,
                _fileCommandIssuer.PrepareKeyValueMessage(name, value));
        }
        else
        {
            _commandIssuer.IssueCommand(
                CommandConstants.SaveState,
                name.ToCommandProperties(),
                value.ToCommandValue());
        }
    }

    /// <inheritdoc />
    public void SetCommandEcho(bool enabled) =>
        _commandIssuer.Issue(
            CommandConstants.Echo, enabled ? "on" : "off");

    /// <inheritdoc />
    public void SetFailed(string message)
    {
        Environment.ExitCode = (int)ExitCode.Failure;
        Error(message);
    }

    /// <inheritdoc />
    public async Task SetOutputAsync(string name, string value)
    {
        var filePath = GetEnvironmentVariable(GITHUB_OUTPUT);
        if (filePath is not null)
        {
            await _fileCommandIssuer.IssueFileCommandAsync(
                OUTPUT,
                _fileCommandIssuer.PrepareKeyValueMessage(name, value));
        }
        else
        {
            _console.WriteLine("");

            _commandIssuer.IssueCommand(
                CommandConstants.SetOutput,
                name.ToCommandProperties(),
                value.ToCommandValue());
        }
    }

    /// <inheritdoc />
    public void SetSecret(string secret) =>
        _commandIssuer.IssueCommand(
            CommandConstants.AddMask, null, secret);

    /// <inheritdoc />
    public void StartGroup(string name) =>
        _commandIssuer.Issue(
            CommandConstants.Group, name);

    /// <inheritdoc />
    public void Warning(string message, AnnotationProperties? properties = default) =>
        _commandIssuer.IssueCommand(
            CommandConstants.Warning, properties?.ToCommandProperties(), message);
}