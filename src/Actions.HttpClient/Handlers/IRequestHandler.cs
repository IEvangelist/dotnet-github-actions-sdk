// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient.Handlers;

internal interface IRequestHandler
{
    void PrepareRequestHeaders(Dictionary<string, IEnumerable<string>> headers);
}
