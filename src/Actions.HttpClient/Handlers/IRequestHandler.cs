// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient.Handlers;

internal interface IRequestHandler
{
    Dictionary<string, IEnumerable<string>> PrepareRequestHeaders(Dictionary<string, IEnumerable<string>> headers);
}
