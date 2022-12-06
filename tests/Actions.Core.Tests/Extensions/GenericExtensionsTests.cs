// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

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
            "David", 7, DateTime.Now, Guid.NewGuid(), new[] { (decimal)Math.PI });

        var commandValue = actual.ToCommandValue();
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var expected = JsonSerializer.Deserialize<SimpleObject>(commandValue, options);

        Assert.Equivalent(expected, actual);
    }
}

file record class SimpleObject(
    string Name,
    int Number,
    DateTime Date,
    Guid Id,
    decimal[] Coordinates);