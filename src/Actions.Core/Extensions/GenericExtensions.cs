// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Core.Extensions;

static class GenericExtensions
{
    /// <summary>
    /// Converts the specified object as:
    /// <list type="bullet">
    /// <item>An empty string, when <see langword="null" />.</item>
    /// <item>The value of the string, when <paramref name="value"/> is a string type.</item>
    /// <item>A JSON string, when the <paramref name="value"/> is an object.</item>
    /// </list>
    /// </summary>
    /// <param name="value">The <paramref name="value"/> in context.</param>
    /// <returns>The string representation of the <paramref name="value"/>.</returns>
    internal static string ToCommandValue(this string? value) =>
        value switch
        {
            null => string.Empty,
            string @string => @string
        };

    /// <summary>
    /// Converts the specified object as:
    /// <list type="bullet">
    /// <item>An empty string, when <see langword="null" />.</item>
    /// <item>The value of the string, when <paramref name="value"/> is a string type.</item>
    /// <item>A JSON string, when the <paramref name="value"/> is an object.</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The generic-type parameter of the given <paramref name="value"/>.</typeparam>
    /// <param name="value">The <paramref name="value"/> in context.</param>
    /// <param name="jsonTypeInfo">The JSON type info of type <typeparamref name="T"/>.</param>
    /// <returns>The string representation of the <paramref name="value"/>.</returns>
    internal static string ToCommandValue<T>(
        this T value, JsonTypeInfo<T> jsonTypeInfo) =>
        JsonSerializer.Serialize(value, jsonTypeInfo);
}