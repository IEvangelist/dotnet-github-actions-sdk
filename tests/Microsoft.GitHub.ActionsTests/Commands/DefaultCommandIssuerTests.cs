// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using Microsoft.GitHub.Actions.Commands;
using Microsoft.GitHub.ActionsTests.Output;

namespace Microsoft.GitHub.ActionsTests.Commands;

public sealed class DefaultCommandIssuerTests
{
    [Fact]
    public void DefaultCommandIssuerIssuesCommandCorrectly()
    {
        var testConsole = new TestConsole();
        var sut = new DefaultCommandIssuer(testConsole);

        sut.IssueCommand(
            command: "command",
            properties: null,
            message: "message");

        Assert.Equal(
            expected: $"::command::message{Environment.NewLine}",
            actual: testConsole.Output.ToString());
    }
}
