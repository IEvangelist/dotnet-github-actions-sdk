// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient;

internal sealed class BearerCredentialHandler(string token) : IRequestHandler
{
    void IRequestHandler.PrepareRequest(RequestOptions options)
    {
        if (options is { Headers: null or { Count: 0 } })
        {
            throw new Exception("The request has no headers");
        }
        
        options.Headers.Add("Authorization", $"Bearer {token}");
    }
}
