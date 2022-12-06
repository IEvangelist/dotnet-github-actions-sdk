// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Glob.Tests;

public sealed class StringExtensionsTests
{
    [Fact]
    public void GetGlobResultThrowsWithInvalidArgsTest() =>
        Assert.Throws<ArgumentNullException>(
            () => (null as string).GetGlobResult(Enumerable.Empty<string>()));

    [Fact]
    public void GetGlobFilesThrowsWithInvalidArgsTest() =>
    Assert.Throws<ArgumentNullException>(
        () => (null as string).GetGlobFiles(null!, null));

    public readonly static IEnumerable<object[]> GetGlobResultTestInput = new[]
    {
        new object[]
        {
            "parent",
            new[] { "**/*" },
            Array.Empty<string>(),
            (true, new string[]
            {
                "file.md",
                "README.md",
                "child/file.MD",
                "child/index.js",
                "child/more.md",
                "child/sample.mtext",
                "child/assets/image.png",
                "child/assets/image.svg",
                "child/grandchild/file.md",
                "child/grandchild/style.css",
                "child/grandchild/sub.text"
            })
        },
        new object[]
        {
            "parent",
            new[] { "**/*child/*.md" },
            Array.Empty<string>(),
            (true, new string[]
            {
                "child/file.MD",
                "child/more.md",
                "child/grandchild/file.md"
            })
        },
        new object[]
        {
            "parent",
            new[] { "**/*/file.md" },
            Array.Empty<string>(),
            (true, new string[]
            {
                "child/file.MD",
                "child/grandchild/file.md"
            })
        },
    };

    [Theory, MemberData(nameof(GetGlobResultTestInput))]
    public void GetGlobResultTest(
        string directory,
        string[] includes,
        string[] exlcudes,
        (bool HasMatches, string[] Files) expected)
    {
        var actual = directory.GetGlobResult(
            includes, exlcudes);

        Assert.Equal(expected.HasMatches, actual.HasMatches);

        var actualFiles = actual.Files.Select(file => file.Path).ToArray();
        Assert.Equal(expected.Files?.Length, actualFiles?.Length);
        if (actual.HasMatches)
        {
            Assert.All(
                expected.Files!,
                expectedFile => Assert.Contains(expectedFile, actualFiles!));
        }
    }

    [Fact]
    public void GetGlobFilesTest()
    {
        var expectedFiles = new[]
        {
            "file.md",
            "README.md",
            "child/file.MD",
            "child/assets/image.svg",
            "child/grandchild/file.md",
        };

        var directory = "parent";
        var actualFiles = directory.GetGlobFiles(
            new[] { "**/*.md", "**/*.svg" },
            new[] { "*/more.md" });

        Assert.Equal(expectedFiles, actualFiles);
    }
}