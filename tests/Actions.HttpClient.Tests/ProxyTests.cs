﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient.Tests;

public class ProxyTests
{
    [Fact]
    public void GetProxyUrlDoesNotReturnUrlWhenVariablesUnset()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);

        var proxyUrl = Proxy.GetProxyUrl(new Uri("https://github.com"));

        Assert.Null(proxyUrl);
    }

    [Fact]
    public void GetProxyUrlReturnsUrlWhenHttpsProxySet()
    {
        Environment.SetEnvironmentVariable("https_proxy", "https://myproxysvr");

        var proxyUrl = Proxy.GetProxyUrl(new Uri("https://github.com"));

        Assert.NotNull(proxyUrl);
    }

    [Fact]
    public void GetProxyUrlDoesNotReturnUrlWhenHttpsProxyUnset()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("http_proxy", "https://myproxysvr");

        var proxyUrl = Proxy.GetProxyUrl(new Uri("https://github.com"));

        Assert.Null(proxyUrl);
    }

    [Fact]
    public void GetProxyUrlReturnsUrlWhenHttpProxySet()
    {
        Environment.SetEnvironmentVariable("http_proxy", "http://myproxysvr");

        var proxyUrl = Proxy.GetProxyUrl(new Uri("http://github.com"));

        Assert.NotNull(proxyUrl);
    }

    [Fact]
    public void GetProxyUrlDoesNotReturnUrlWhenHttpsProxySetAndInNoProxyList()
    {
        Environment.SetEnvironmentVariable("https_proxy", "https://myproxysvr");
        Environment.SetEnvironmentVariable("no_proxy", "otherserver,myserver,anotherserver:8080");

        var proxyUrl = Proxy.GetProxyUrl(new Uri("https://myserver"));

        Assert.Null(proxyUrl);
    }

    [Fact]
    public void GetProxyUrlDoesNotReturnUrlWhenHttpsProxySetAndNotInNoProxyList()
    {
        Environment.SetEnvironmentVariable("https_proxy", "https://myproxysvr");
        Environment.SetEnvironmentVariable("no_proxy", "otherserver,myserver,anotherserver:8080");

        var proxyUrl = Proxy.GetProxyUrl(new Uri("https://github.com"));

        Assert.NotNull(proxyUrl);
    }

    [Fact]
    public void GetProxyUrlDoesNotReturnUrlWhenHttpProxySetAndInNoProxyList()
    {
        Environment.SetEnvironmentVariable("http_proxy", "http://myproxysvr");
        Environment.SetEnvironmentVariable("no_proxy", "otherserver,myserver,anotherserver:8080");

        var proxyUrl = Proxy.GetProxyUrl(new Uri("http://myserver"));

        Assert.Null(proxyUrl);
    }

    [Fact]
    public void GetProxyUrlDoesNotReturnUrlWhenHttpProxySetAndNotInNoProxyList()
    {
        Environment.SetEnvironmentVariable("http_proxy", "http://myproxysvr");
        Environment.SetEnvironmentVariable("no_proxy", "otherserver,myserver,anotherserver:8080");

        var proxyUrl = Proxy.GetProxyUrl(new Uri("http://github.com"));

        Assert.NotNull(proxyUrl);
    }

    [Fact]
    public void GetProxyUrlReturnsUrlWhenHttpProxyHasNoProtocol()
    {
        Environment.SetEnvironmentVariable("http_proxy", "myproxysvr");

        var proxyUrl = Proxy.GetProxyUrl(new Uri("http://github.com"));

        Assert.Equal("http://myproxysvr/", proxyUrl!.ToString());
    }

    [Fact]
    public void CheckBypassReturnsTrueWhenHostIsNoProxyList()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "myserver");

        var bypass = Proxy.CheckBypass(new Uri("https://myserver"));

        Assert.True(bypass);
    }

    [Fact]
    public void CheckBypassReturnsTrueWhenHostInNoProxyList()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "otherserver,myserver,anotherserver:8080");

        var bypass = Proxy.CheckBypass(new Uri("https://myserver"));

        Assert.True(bypass);
    }

    [Fact]
    public void CheckBypassReturnsTrueWhenHostInNoProxyListWithSpaces()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "otherserver, myserver ,anotherserver:8080");

        var bypass = Proxy.CheckBypass(new Uri("https://myserver"));

        Assert.True(bypass);
    }

    [Fact]
    public void CheckBypassReturnsTrueWhenHostInNoProxyListWithPort()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "otherserver, myserver:8080 ,anotherserver");

        var bypass = Proxy.CheckBypass(new Uri("https://myserver:8080"));

        Assert.True(bypass);
    }

    [Fact]
    public void CheckBypassReturnsTrueWhenHostInNoProxyListWithoutPort()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "otherserver, myserver ,anotherserver");

        var bypass = Proxy.CheckBypass(new Uri("https://myserver:8080"));

        Assert.True(bypass);
    }

    [Fact]
    public void CheckBypassReturnsTrueWhenHostInNoProxyListWithHttpsPort()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "otherserver, myserver:443 ,anotherserver");

        var bypass = Proxy.CheckBypass(new Uri("https://myserver"));

        Assert.True(bypass);
    }

    [Fact]
    public void CheckBypassReturnsTrueWhenHostInNoProxyListWithHttpPort()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "otherserver, myserver:80 ,anotherserver");

        var bypass = Proxy.CheckBypass(new Uri("http://myserver"));

        Assert.True(bypass);
    }

    [Fact]
    public void CheckBypassReturnsFalseWhenHostNotInNoProxyList()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "otherserver, myserver ,anotherserver:8080");

        var bypass = Proxy.CheckBypass(new Uri("https://github.com"));

        Assert.False(bypass);
    }

    [Fact]
    public void CheckBypassReturnsTrueWhenHostWithSubdomainInNoProxyList()
    {
        Environment.SetEnvironmentVariable("https_proxy", null);
        Environment.SetEnvironmentVariable("no_proxy", "myserver.com'");

        var bypass = Proxy.CheckBypass(new Uri("https://sub.myserver.com"));

        Assert.True(bypass);
    }
}