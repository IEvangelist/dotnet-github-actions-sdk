// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.ActionsTests.Commands;

public sealed class DefaultFileCommandIssuerTests
{
    [Fact]
    public async Task IssueFileCommandAsyncCorrectlyWritesFileTest()
    {
        var path = "test-file.txt";
        Environment.SetEnvironmentVariable(
            $"GITHUB_TEST",
            path);
        await File.AppendAllTextAsync(path, "");

        IFileCommandIssuer sut = new DefaultFileCommandIssuer(
            (filePath, actual) =>
            {
                Assert.NotNull(filePath);

                var expected = """{"test":"Values","number":7}""";
                Assert.Equal(expected, actual);

                return Task.CompletedTask;
            });

        await sut.IssueFileCommandAsync(
            commandSuffix: "TEST",
            message: new { Test = "Values", Number = 7 });
    }
}
