// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient.Handlers;

internal sealed class BasicCredentialHandler(string username, string password) : IRequestHandler
{
    void IRequestHandler.PrepareRequestHeaders(Dictionary<string, IEnumerable<string>> headers)
    {
        ArgumentNullException.ThrowIfNull(headers);

        headers["Authorization"] =
        [
            new AuthenticationHeaderValue(
                "Basic",
                $"{username}:{password}".ToBase64()
            )
            .ToString()
        ];
    }
}
