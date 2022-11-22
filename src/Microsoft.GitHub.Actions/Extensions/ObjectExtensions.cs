// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Extensions;

internal static class ObjectExtensions
{
    internal static Dictionary<string, string> ToCommandProperties<T>(
        this T value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null) =>
        new()
        {
            [paramName!] = value?.ToString() ?? string.Empty
        };
}
