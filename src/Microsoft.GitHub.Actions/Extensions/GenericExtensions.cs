// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Extensions;

static class GenericExtensions
{
    static readonly Lazy<JsonSerializerOptions> s_lazyOptions =
        new(() => new(JsonSerializerDefaults.Web)
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

    /// <summary>
    /// Converts the specified object as:
    /// <list type="bullet">
    /// <item>An empty string, when <c>null</c>.</item>
    /// <item>The value of the string, when <paramref name="value"/> is a string type.</item>
    /// <item>A JSON string, when the <paramref name="value"/> is an object.</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The generic-type parameter of the given <paramref name="value"/>.</typeparam>
    /// <param name="value">The <paramref name="value"/> in context.</param>
    /// <returns>The string representation of the <paramref name="value"/>.</returns>
    internal static string ToCommandValue<T>(this T? value) => value switch
    {
        null => string.Empty,
        string @string => @string,
        _ => JsonSerializer.Serialize(value, s_lazyOptions.Value)
    };
}