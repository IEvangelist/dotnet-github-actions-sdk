// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.ActionsTests.Commands;

public sealed class DefaultCommandIssuerTests
{
    [Fact]
    public void IssuesCorrectly()
    {
        var testConsole = new TestConsole();
        ICommandIssuer sut = new DefaultCommandIssuer(testConsole);

        sut.Issue(
            commandName: "command",
            message: "message");

        Assert.Equal(
            expected: $"""
                Issuing unconventional command.{Environment.NewLine}::command::message{Environment.NewLine}
                """,
            actual: testConsole.Output.ToString());
    }

    [Fact]
    public void IssuesCommandCorrectly()
    {
        var testConsole = new TestConsole();
        ICommandIssuer sut = new DefaultCommandIssuer(testConsole);

        sut.IssueCommand(
            commandName: "command",
            properties: null,
            message: "message");

        Assert.Equal(
            expected: $"""
                Issuing unconventional command.{Environment.NewLine}::command::message{Environment.NewLine}
                """,
            actual: testConsole.Output.ToString());
    }

    public static IEnumerable<object[]> WritesOutputInput = new[]
    {
        new object[]
        {
            new Dictionary<string, string>
            {
                ["name"] = "summary"
            },
            "Everything worked as expected",
            $"::{CommandNames.SetOutput} name=summary::Everything worked as expected"
        },
        new object[]
        {
            null!,
            "deftones",
            $"::{CommandNames.SetOutput}::deftones"
        },
        new object[]
        {
            new Dictionary<string, string>
            {
                ["name"] = "percent % percent % cr \r cr \r lf \n lf \n colon : colon : comma , comma ,"
            },
            null!,
            $"::{CommandNames.SetOutput} name=percent %25 percent %25 cr %0D cr %0D lf %0A lf %0A colon %3A colon %3A comma %2C comma %2C::"
        },
        new object[]
        {
            null!,
            "%25 %25 %0D %0D %0A %0A %3A %3A %2C %2C",
            $"::{CommandNames.SetOutput}::%2525 %2525 %250D %250D %250A %250A %253A %253A %252C %252C"
        },
        new object[]
        {
            new Dictionary<string, string>
            {
                ["prop1"] = "Value 1",
                ["prop2"] = "Value 2"
            },
            "example",
            $"::{CommandNames.SetOutput} prop1=Value 1,prop2=Value 2::example"
        },
        new object[]
        {
            new Dictionary<string, string>
            {
                ["prop1"] = JsonSerializer.Serialize(new { test = "object"}),
                ["prop2"] = "123",
                ["prop3"] = "true"
            },
            JsonSerializer.Serialize(new { test = "object"}).ToCommandValue(),
            $$"""
            ::{{CommandNames.SetOutput}} prop1={"test"%3A"object"},prop2=123,prop3=true::{"test":"object"}
            """
        }
    };

    [Theory]
    [MemberData(nameof(WritesOutputInput))]
    public void IssuesCommandWithPropertiesCorrectly(
        Dictionary<string, string>? properties = null,
        string? message = null,
        string? expected = null)
    {
        var testConsole = new TestConsole();
        ICommandIssuer sut = new DefaultCommandIssuer(testConsole);

        sut.IssueCommand(
            commandName: CommandNames.SetOutput,
            properties,
            message);

        Assert.Equal(
            expected: $"""
                {expected}{Environment.NewLine}
                """,
            actual: testConsole.Output.ToString());
    }
}
