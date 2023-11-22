// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Actions.Core.Extensions;

namespace Actions.Core.Tests.Extensions;

public sealed class GenericExtensionsTests
{
    [Fact]
    public void ToCommandValueCorrectlyReturnsEmptyStringWhenNullTest()
    {
        string? value = null;
        var actual = value.ToCommandValue();
        Assert.Equal(string.Empty, actual);
    }

    [Fact]
    public void ToCommandValueCorrectlyReturnsStringValueTest()
    {
        var value = "Hello!";
        var actual = value.ToCommandValue();
        Assert.Equal("Hello!", actual);
    }

    [Fact]
    public void ToCommandValueCorrectlySerializesValueTest()
    {
        SimpleObject actual = new(
            "David", 7, DateTime.Now, Guid.NewGuid(), [(decimal)Math.PI]);

        var typeInfo = SimpleContext.Default.SimpleObject;
        var commandValue = actual.ToCommandValue<SimpleObject>(typeInfo);

        var expected = JsonSerializer.Deserialize<SimpleObject>(
            commandValue, typeInfo.Options);

        Assert.Equivalent(expected, actual);
    }
}

internal record class SimpleObject(
    string Name,
    int Number,
    DateTime Date,
    Guid Id,
    decimal[] Coordinates);

[JsonSerializable(typeof(SimpleObject))]
internal partial class SimpleContext : JsonSerializerContext
{
}