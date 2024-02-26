// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient;

public record class TypedResponse<T>(
    int StatusCode,
    T? Result,
    IDictionary<string, string> IncomingHttpHeaders);
