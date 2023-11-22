// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Core.Tests.Commands;

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

                return ValueTask.CompletedTask;
            });

        var message = JsonSerializer.Serialize(
            new { test = "Values", number = 7 });

        await sut.IssueFileCommandAsync(
            commandSuffix: "TEST",
            message);
    }

    [Fact]
    public async Task SetOutputCorrectlySetsOutputTest()
    {
        var path = "set-output-test.txt";
        try
        {
            Environment.SetEnvironmentVariable(
                "GITHUB_OUTPUT",
                path);
            await File.AppendAllTextAsync(path, "");

            var services = new ServiceCollection();
            services.AddGitHubActionsCore();

            var provider = services.BuildServiceProvider();
            var core = provider.GetRequiredService<ICoreService>();

            await core.SetOutputAsync("has-remaining-work", "true");
            await core.SetOutputAsync("upgrade-projects", JsonSerializer.Serialize(new[]
            {
                "this/is/a/test.csproj",
                "another/test/example.csproj"
            }));

            var lines = await File.ReadAllLinesAsync(path);
            Assert.NotNull(lines);
            Assert.StartsWith("has-remaining-work", lines[0]);
            Assert.Equal("true", lines[1]);
            Assert.StartsWith("upgrade-projects", lines[3]);
            Assert.Equal("""["this/is/a/test.csproj","another/test/example.csproj"]""", lines[4]);
        }
        finally
        {
            File.Delete(path);

            Environment.SetEnvironmentVariable(
                "GITHUB_OUTPUT",
                null);
        }        
    }
}
