﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Core.Workflows;

/// <summary>
/// Command format:
/// <c>::name key=value,key=value::message</c>
/// </summary>
/// <example>
/// <list type="bullet">
/// <item><c>::warning::This is the message</c></item>
/// <item><c>::set-env name=MY_VAR::some value</c></item>
/// </list>
/// </example>
internal readonly record struct Command(
    string? CommandName = "missing.command",
    string? Message = default,
    IReadOnlyDictionary<string, string>? CommandProperties = default)
{
    const string CMD_STRING = "::";

    internal bool Conventional =>
        CommandNames.IsConventional(CommandName);

    /// <summary>
    /// The string representation of the workflow command, i.e.;
    /// <code>::name key=value,key=value::message</code>.
    /// </summary>
    public override string ToString()
    {
        StringBuilder builder = new($"{CMD_STRING}{CommandName}");

        if (CommandProperties?.Any() ?? false)
        {
            builder.Append(' ');
            foreach (var (isNotFirst, key, value)
                in CommandProperties.Select(
                    (kvp, index) => (index is > 0, kvp.Key, kvp.Value)))
            {
                if (isNotFirst)
                {
                    builder.Append(',');
                }
                builder.Append($"{key}={EscapeProperty(value)}");
            }
        }

        builder.Append($"{CMD_STRING}{EscapeData(Message)}");

        return builder.ToString();
    }

    static string EscapeProperty(string? value) =>
        value.ToCommandValue()
            .Replace("%", "%25")
            .Replace("\r", "%0D")
            .Replace("\n", "%0A")
            .Replace(":", "%3A")
            .Replace(",", "%2C");

    static string EscapeData(string? value) =>
        value.ToCommandValue()
            .Replace("%", "%25")
            .Replace("\r", "%0D")
            .Replace("\n", "%0A");
}
