// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using Microsoft.GitHub.Actions.Workflows;

namespace Microsoft.GitHub.ActionsTests.Workflows;

public sealed class CommandTests
{
    public static IEnumerable<object[]> CommandToStringInput = new[]
    {
        new object[]
        {
            "some-cmd", 7, null!, "::some-cmd::7",
        },
        new object[]
        {
            "another-name", true, null!, "::another-name::true"
        },
        new object[]
        {
            "cmdr", false, new Dictionary<string, string> { ["k1"] = "v1" }, "::cmdr k1=v1::false"
        },
        new object[]
        {
            "~~~", "Hi friends!", null!, "::~~~::Hi friends!"
        },
        new object[]
        {
            null!, null!, null!, "::::"
        }
    };

    [Theory]
    [MemberData(nameof(CommandToStringInput))]
    public void CommandToStringTest<T>(
        string? name = null,
        T message = default!,
        Dictionary<string, string>? properties = null,
        string? expected = null)
    {
        Command<T> command = new(name, message, properties);

        var actual = command.ToString();

        Assert.Equal(expected, actual);
    }
}
