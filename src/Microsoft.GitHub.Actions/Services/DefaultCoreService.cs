// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Services;

/// <inheritdoc cref="ICoreService" />
internal sealed class DefaultCoreService : ICoreService
{
    private readonly IConsole _console;
    private readonly ICommandIssuer _commandIssuer;
    private readonly IFileCommandIssuer _fileCommandIssuer;

    public DefaultCoreService(
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
            _commandIssuer.IssueCommand(CommandNames.AddPath, null, path);
        }

        SetEnvironmentVariable(
            PATH,
            $"{path}{PathSeparator}{GetEnvironmentVariable(PATH)}");
    }

    /// <inheritdoc />
    public void Debug(string message) =>
        _commandIssuer.IssueCommand(
            CommandNames.Debug, message: message);

    /// <inheritdoc />
    public void EndGroup() =>
        _commandIssuer.Issue(
            CommandNames.EndGroup, "");

    /// <inheritdoc />
    public void Error(string message, AnnotationProperties? properties = default) =>
        _commandIssuer.IssueCommand(
            CommandNames.Error, properties?.ToCommandProperties(), message);

    /// <inheritdoc />
    public async ValueTask ExportVariableAsync(string name, string value)
    {
        value = value.ToCommandValue();
        SetEnvironmentVariable(name, value);

        var filePath = GetEnvironmentVariable(GITHUB_ENV);
        if (filePath is not null)
        {
            await _fileCommandIssuer.IssueFileCommandAsync(
                ENV,
                _fileCommandIssuer.PrepareKeyValueMessage(name, value));
        }
        else
        {
            _commandIssuer.IssueCommand<string>(
                CommandNames.SetEnv,
                name.ToCommandProperties());
        }
    }

    /// <inheritdoc />
    public bool GetBoolInput(string name, InputOptions? options = default)
    {
        var value = GetInput(name, options);

        return bool.TryParse(value, out var result)
            ? result
            : throw new Exception($"""
                Input does not meet YAML 1.2 "Core Schema" specification: {name}
                Support boolean input list: \`true | True | TRUE | false | False | FALSE\`
                """);
    }

    /// <inheritdoc />
    public string GetInput(string name, InputOptions? options = default)
    {
        var value = GetEnvironmentVariable(
            $"{INPUT_}{name.Replace(' ', '_').ToUpperInvariant()}");

        return options.HasValue && options.Value is { Required: true } &&
            string.IsNullOrWhiteSpace(value)
            ? throw new Exception(
                $"Input required and not supplied: {name}")
            : options.HasValue && options.Value is { TrimWhitespace: false }
                ? value ?? ""
                : value?.Trim() ?? "";
    }

    /// <inheritdoc />
    public string[] GetMultilineInput(string name, InputOptions? options = default)
    {
        var inputs = GetInput(name, options)
            .Split('\n', StringSplitOptions.RemoveEmptyEntries);

        return options.HasValue && options.Value is { TrimWhitespace: false }
            ? inputs
            : inputs.Select(input => input.Trim()).ToArray();
    }

    /// <inheritdoc />
    public string GetState(string name) =>
        GetEnvironmentVariable($"{STATE_}{name}") ?? "";

    /// <inheritdoc />
    public async ValueTask<T> GroupAsync<T>(string name, Func<ValueTask<T>> task)
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
            CommandNames.Notice, properties?.ToCommandProperties(), message);

    /// <inheritdoc />
    public async ValueTask SaveStateAsync<T>(string name, T value)
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
                CommandNames.SaveState,
                name.ToCommandProperties(),
                value.ToCommandValue());
        }
    }

    /// <inheritdoc />
    public void SetCommandEcho(bool enabled) =>
        _commandIssuer.Issue(
            CommandNames.Echo, enabled ? "on" : "off");

    /// <inheritdoc />
    public void SetFailed(string message)
    {
        Environment.ExitCode = (int)ExitCode.Failure;
        Error(message);
    }

    /// <inheritdoc />
    public async ValueTask SetOutputAsync<T>(string name, T value)
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
                CommandNames.SetOutput,
                name.ToCommandProperties(),
                value.ToCommandValue());
        }
    }

    /// <inheritdoc />
    public void SetSecret(string secret) =>
        _commandIssuer.IssueCommand(
            CommandNames.AddMask, null, secret);

    /// <inheritdoc />
    public void StartGroup(string name) =>
        _commandIssuer.Issue(
            CommandNames.Group, name);

    /// <inheritdoc />
    public void Warning(string message, AnnotationProperties? properties = default) =>
        _commandIssuer.IssueCommand(
            CommandNames.Warning, properties?.ToCommandProperties(), message);
}