// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using Microsoft.GitHub.Actions.Commands;
using Microsoft.GitHub.ActionsTests.Output;

namespace Microsoft.GitHub.ActionsTests.Commands;

public sealed class DefaultCommandIssuerTests
{
    [Fact]
    public void DefaultCommandIssuerIssuesCorrectly()
    {
        var testConsole = new TestConsole();
        ICommandIssuer sut = new DefaultCommandIssuer(testConsole);

        sut.Issue(
            name: "command",
            message: "message");

        Assert.Equal(
            expected: $"""
                Issuing unconventional command.{Environment.NewLine}::command::message{Environment.NewLine}
                """,
            actual: testConsole.Output.ToString());
    }

    [Fact]
    public void DefaultCommandIssuerIssuesCommandCorrectly()
    {
        var testConsole = new TestConsole();
        ICommandIssuer sut = new DefaultCommandIssuer(testConsole);

        sut.IssueCommand(
            command: "command",
            properties: null,
            message: "message");

        Assert.Equal(
            expected: $"""
                Issuing unconventional command.{Environment.NewLine}::command::message{Environment.NewLine}
                """,
            actual: testConsole.Output.ToString());
    }
}
