// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Core.Commands;

/// <inheritdoc cref="IFileCommandIssuer" />
internal sealed class DefaultFileCommandIssuer : IFileCommandIssuer
{
    private readonly Func<string, string, ValueTask> _writeLineTask;

    public DefaultFileCommandIssuer(
        Func<string, string, ValueTask> writeLineTask) =>
        _writeLineTask = writeLineTask.ThrowIfNull();

    /// <inheritdoc />
    ValueTask IFileCommandIssuer.IssueFileCommandAsync<TValue>(
        string commandSuffix, TValue message)
    {
        var filePath = GetEnvironmentVariable($"{GITHUB_}{commandSuffix}");
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new Exception(
                "Unable to find environment variable for file " +
                $"command suffix '{commandSuffix} ({GITHUB_}{commandSuffix})'.");
        }

        if (File.Exists(filePath) is false)
        {
            throw new Exception(
                $"Missing file at path: '{filePath}' for file command '{commandSuffix}'.");
        }
        
        return _writeLineTask.Invoke(filePath, message.ToCommandValue());
    }

    /// <inheritdoc />
    string IFileCommandIssuer.PrepareKeyValueMessage<TValue>(string key, TValue value)
    {
        var delimiter = $"ghadelimiter_{Guid.NewGuid()}";
        var convertedValue = value.ToCommandValue();

        // These should realistically never happen, but just in case someone finds a
        // way to exploit uuid generation let's not allow keys or values that contain
        // the delimiter.
        if (key.Contains(delimiter, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Unexpected input: name should not contain the delimiter {delimiter}");
        }

        if (convertedValue.Contains(delimiter, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception(
                $"Unexpected input: value should not contain the delimiter {delimiter}");
        }

        return $"{key}<<{delimiter}{NewLine}{convertedValue}{NewLine}{delimiter}";
    }
}
