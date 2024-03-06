// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Glob.Tests;

public sealed class StringExtensionsTests
{
    public readonly static TheoryData<string, string[], string[], (bool, string[])> GetGlobResultTestInput =
    new()
    {
        {
            "parent",
            new[] { "**/*" },
            Array.Empty<string>(),
            (true, new string[]
            {
                "parent/file.md",
                "parent/README.md",
                "parent/child/file.MD",
                "parent/child/index.js",
                "parent/child/more.md",
                "parent/child/sample.mtext",
                "parent/child/assets/image.png",
                "parent/child/assets/image.svg",
                "parent/child/grandchild/file.md",
                "parent/child/grandchild/style.css",
                "parent/child/grandchild/sub.text"
            })
        },
        {
            "parent",
            new[] { "**/*child/*.md" },
            Array.Empty<string>(),
            (true, new string[]
            {
                "parent/child/file.MD",
                "parent/child/more.md",
                "parent/child/grandchild/file.md"
            })
        },
        {
            "parent",
            new[] { "**/*/file.md" },
            Array.Empty<string>(),
            (true, new string[]
            {
                "parent/child/file.MD",
                "parent/child/grandchild/file.md"
            })
        }
    };

    [Theory, MemberData(nameof(GetGlobResultTestInput))]
    public void GetGlobResultTest(
        string directory,
        string[] includes,
        string[] excludes,
        (bool HasMatches, string[] Files) expected)
    {
        var actual = directory.GetGlobResult(
            includes, excludes);

        Assert.Equal(expected.HasMatches, actual.HasMatches);

        var actualFiles = actual.Files.Select(file => file.FullName).ToArray();
        Assert.Equal(expected.Files?.Length, actualFiles?.Length);
        if (actual.HasMatches)
        {
            string[] expectedFiles =
            [
                .. expected.Files!.Select(
                static file => Path.GetFullPath(file))
            ];

            Assert.All(
                expectedFiles,
                expectedFile => Assert.Contains(expectedFile, actualFiles!));
        }
    }

    [Fact]
    public void GetGlobFilesTest()
    {
        var expectedFiles = new[]
        {
            "parent/file.md",
            "parent/README.md",
            "parent/child/file.MD",
            "parent/child/assets/image.svg",
            "parent/child/grandchild/file.md",
        };

        var directory = "parent";
        var actualFiles = directory.GetGlobFiles(
                ["**/*.md", "**/*.svg"],
                ["*/more.md"])
            .ToArray();

        string[] expected =
        [
            .. expectedFiles.Select(
                        static file => Path.GetFullPath(file))
        ];

        Assert.Equal(expectedFiles.Length, actualFiles.Length);
        Assert.All(
            expected,
            expectedFile => Assert.Contains(expectedFile, actualFiles!));
    }
}
