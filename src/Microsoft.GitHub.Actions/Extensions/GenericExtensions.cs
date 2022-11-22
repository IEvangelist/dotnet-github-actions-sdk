// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Text.Encodings.Web;
using System.Text.Json;

namespace Microsoft.GitHub.Actions.Extensions;

static class GenericExtensions
{
    static readonly Lazy<JsonSerializerOptions> s_lazyOptions =
        new(() => new(JsonSerializerDefaults.Web)
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

    internal static string ToCommandValue<T>(this T? value) => value switch
    {
        null => string.Empty,
        string @string => @string,
        _ => JsonSerializer.Serialize(value, s_lazyOptions.Value)
    };
}