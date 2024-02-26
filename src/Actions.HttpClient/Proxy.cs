// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient;

internal sealed class Proxy
{
    public Uri? GetProxyUrl(Uri requestUrl)
    {
        var usingSsl = requestUrl.Scheme is "https";

        if (CheckBypass(requestUrl))
        {
            return null;
        }

        var proxyVar = GetProxyVariable(usingSsl);

        static string? GetProxyVariable(bool usingSsl)
        {
            return usingSsl
                ? Environment.GetEnvironmentVariable("https_proxy")
                ?? Environment.GetEnvironmentVariable("HTTPS_PROXY")
                : Environment.GetEnvironmentVariable("http_proxy")
                ?? Environment.GetEnvironmentVariable("HTTP_PROXY");
        }

        if (proxyVar is not null)
        {
            try
            {
                return new Uri(proxyVar);
            }
            catch
            {
                if (proxyVar.StartsWith("http://") is false &&
                    proxyVar.StartsWith("https://") is false)
                {
                    return new Uri($"http://{proxyVar}");
                }
            }
        }

        return null;
    }

    private static bool CheckBypass(Uri requestUrl)
    {
        var hostName = requestUrl.Host;

        if (hostName is null)
        {
            return false;
        }

        if (requestUrl.IsLoopback)
        {
            return true;
        }

        var noProxy = Environment.GetEnvironmentVariable("no_proxy")
            ?? Environment.GetEnvironmentVariable("NO_PROXY");

        if (noProxy is null)
        {
            return false;
        }

        string[] upperReqHosts =
        [
            requestUrl.Host.ToUpper(),
            $"{requestUrl.Host.ToUpper()}:{requestUrl.Port}"
        ];

        var upperNoProxyItems = noProxy.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim().ToUpper())
            .Where(x => !string.IsNullOrEmpty(x));

        foreach (var upperNoProxyItem in upperNoProxyItems)
        {
            if (upperNoProxyItem is "*" ||
                upperReqHosts.Any(x => x == upperNoProxyItem ||
                    x.EndsWith($".{upperNoProxyItem}") ||
                (upperNoProxyItem.StartsWith('.') && x.EndsWith(upperNoProxyItem))))
            {
                return true;
            }
        }

        return false;
    }
}
