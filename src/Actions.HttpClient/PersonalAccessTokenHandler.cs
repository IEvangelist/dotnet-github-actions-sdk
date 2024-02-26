// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Text;

namespace Actions.HttpClient;

internal sealed class PersonalAccessTokenHandler(string token) : IRequestHandler
{
    void IRequestHandler.PrepareRequest(RequestOptions options)
    {
        if (options is { Headers: null or { Count: 0 } })
        {
            throw new Exception("The request has no headers");
        }

        options.Headers.Add("Authorization", $"Basic {
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"PAT:{token}"))}");
    }
}