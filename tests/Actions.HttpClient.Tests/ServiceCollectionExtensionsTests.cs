// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using Actions.HttpClient.Clients;
using Actions.HttpClient.Factories;

namespace Actions.HttpClient.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddHttpClientServices_CorrectlyRegistersExpectedClients()
    {
        // Arrange
        var services = new ServiceCollection()
            .AddHttpClientServices();

        // Act
        var provider = services.BuildServiceProvider();
        var factory = provider.GetRequiredService<IHttpCredentialClientFactory>();

        // Assert
        Assert.NotNull(factory);
        Assert.IsType<DefaultHttpKeyedClientFactory>(factory);

        var basicClient = factory.CreateBasicClient("username", "password");
        Assert.NotNull(basicClient);
        Assert.IsType<DefaultHttpClient>(basicClient);
    }
}
