// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient.Tests;

public sealed record class PostmanEchoResponse(
    RequestData Data,
    Args Args,
    Dictionary<string, string> Headers,
    string Url);
