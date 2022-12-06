// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Glob.Tests;

public sealed class GlobPatternBuilderTests
{
    [Fact]
    public void GlobPatternBuilderThrowsWithoutIncludeOrExcludePatternsTest() =>
        Assert.Throws<ArgumentException>(
            () => new DefaultGlobPatternResolverBuilder().Build());

    [Fact]
    public void GlobPatternBuilderYieldsWorkingResolver()
    {
        var resolver = new DefaultGlobPatternResolverBuilder()
            .With("**/*.md", "**/*.svg")
            .Without("*/more.md")
            .Build();

        var result = resolver.GetGlobResult("parent");
        Assert.True(result.HasMatches);
        Assert.Equal(5, result.Files.Count());

        var expectedFiles = new[]
        {
            "file.md",
            "README.md",
            "child/file.MD",
            "child/assets/image.svg",
            "child/grandchild/file.md",
        };
        Assert.All(
            expectedFiles,
            expectedFile => Assert.Contains(
                expectedFile, result.Files.Select(f => f.Path)));
    }
}
