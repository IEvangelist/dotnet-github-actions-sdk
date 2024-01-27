// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Core.Tests.Services;

public class CoreSummaryTests(CoreSummaryTestFixture fixture) : IClassFixture<CoreSummaryTestFixture>
{
    private async Task AssertSummary(string expected)
    {
        var file = await File.ReadAllTextAsync(fixture.TestFilePath);

        Assert.Equal(expected, file);
    }

    [Fact]
    public Task ThrowsIfSummaryEnvVarIsUndefined() => fixture.TestAsync(async () =>
    {
        Environment.SetEnvironmentVariable(GITHUB_STEP_SUMMARY, null);

        Task<Summary> WriteAsync()
        {
            var sut = new Summary();

            return sut.AddRaw(fixture.TestCase.Text).WriteAsync();
        }

        await Assert.ThrowsAsync<Exception>(WriteAsync);
    });

    [Fact]
    public Task ThrowsIfSummaryFileDoesNotExist() => fixture.TestAsync(async () =>
    {
        File.Delete(fixture.TestFilePath);

        Task<Summary> WriteAsync()
        {
            var sut = new Summary();

            return sut.AddRaw(fixture.TestCase.Text).WriteAsync();
        }

        await Assert.ThrowsAsync<Exception>(WriteAsync);
    });

    [Fact]
    public Task AppendsTextToSummaryFile() => fixture.TestAsync(async () =>
    {
        await File.WriteAllTextAsync(fixture.TestFilePath, "# ");

        var sut = new Summary();

        await sut.AddRaw(fixture.TestCase.Text).WriteAsync();

        await AssertSummary($"# {fixture.TestCase.Text}");
    });

    [Fact]
    public Task OverwritesTextToSummaryFile() => fixture.TestAsync(async () =>
    {
        await File.WriteAllTextAsync(fixture.TestFilePath, "overwrite");

        var sut = new Summary();

        await sut.AddRaw(fixture.TestCase.Text).WriteAsync(new() { Overwrite = true });

        await AssertSummary(fixture.TestCase.Text);
    });

    [Fact]
    public Task AppendsTextWithEOLToSummaryFile() => fixture.TestAsync(async () =>
    {
        await File.WriteAllTextAsync(fixture.TestFilePath, "# ");

        var sut = new Summary();

        await sut.AddRaw(fixture.TestCase.Text, true).WriteAsync();

        await AssertSummary($"# {fixture.TestCase.Text}{Environment.NewLine}");
    });

    [Fact]
    public Task ChainsAppendsTextToSummaryFile() => fixture.TestAsync(async () =>
    {
        await File.WriteAllTextAsync(fixture.TestFilePath, "");

        var sut = new Summary();

        await sut
            .AddRaw(fixture.TestCase.Text)
            .AddRaw(fixture.TestCase.Text)
            .AddRaw(fixture.TestCase.Text)
            .WriteAsync();

        await AssertSummary(string.Join("", fixture.TestCase.Text, fixture.TestCase.Text, fixture.TestCase.Text));
    });

    [Fact]
    public Task EmptiesBufferAfterWriteAsync() => fixture.TestAsync(async () =>
    {
        await File.WriteAllTextAsync(fixture.TestFilePath, "");

        var sut = new Summary();

        await sut.AddRaw(fixture.TestCase.Text).WriteAsync();

        await AssertSummary(fixture.TestCase.Text);

        Assert.True(sut.IsBufferEmpty);
    });

    [Fact]
    public void ReturnsSummaryBufferAsString() => fixture.Test(() =>
    {
        var sut = new Summary();

        sut.AddRaw(fixture.TestCase.Text);

        Assert.Equal(fixture.TestCase.Text, sut.Stringify());
    });

    [Fact]
    public void ReturnsCorrectValuesForIsBufferEmpty() => fixture.Test(() =>
    {
        var sut = new Summary();

        sut.AddRaw(fixture.TestCase.Text);

        Assert.False(sut.IsBufferEmpty);

        sut.EmptyBuffer();

        Assert.True(sut.IsBufferEmpty);
    });

    [Fact]
    public Task ClearsABufferAndSummaryFile() => fixture.TestAsync(async () =>
    {
        await File.WriteAllTextAsync(fixture.TestFilePath, "content", Encoding.UTF8);

        var sut = new Summary();

        await sut.ClearAsync();

        await AssertSummary("");

        Assert.True(sut.IsBufferEmpty);
    });

    [Fact]
    public Task AddsEOL() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddRaw(fixture.TestCase.Text).AddNewLine().WriteAsync();

        await AssertSummary($"{fixture.TestCase.Text}{Environment.NewLine}");
    });

    [Fact]
    public Task AddsACodeBlockWithoutLanguage() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddCodeBlock(fixture.TestCase.Code).WriteAsync();

        var expected = $$"""
            <pre><code>func fork() {
                for {
                    go fork()
                }
            }</code></pre>{{Environment.NewLine}}
            """;

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownCodeBlockWithoutLanguage() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownCodeBlock(fixture.TestCase.Code).WriteAsync();

        var expected = $$"""
            ```
            func fork() {
                for {
                    go fork()
                }
            }
            ```{{Environment.NewLine}}
            """;

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsACodeBlockWithALanguage() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddCodeBlock(fixture.TestCase.Code, "go").WriteAsync();

        var expected = $$"""
            <pre lang="go"><code>func fork() {
                for {
                    go fork()
                }
            }</code></pre>{{Environment.NewLine}}
            """;

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownCodeBlockWithALanguage() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownCodeBlock(fixture.TestCase.Code, "go").WriteAsync();

        var expected = $$"""
            ```go
            func fork() {
                for {
                    go fork()
                }
            }
            ```{{Environment.NewLine}}
            """;

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsAnUnorderedList() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddList(fixture.TestCase.List).WriteAsync();

        var expected = $"<ul><li>foo</li><li>bar</li><li>baz</li><li>💣</li></ul>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownUnorderedList() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownList(fixture.TestCase.List).WriteAsync();

        var expected = $"- foo\n- bar\n- baz\n- 💣{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsAnOrderedList() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddList(fixture.TestCase.List, true).WriteAsync();

        var expected = $"<ol><li>foo</li><li>bar</li><li>baz</li><li>💣</li></ol>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownOrderedList() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownList(fixture.TestCase.List, true).WriteAsync();

        var expected = $"1. foo\n1. bar\n1. baz\n1. 💣{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownTaskList() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownTaskList(fixture.TestCase.Tasks).WriteAsync();

        var expected = $"- [ ] foo\n- [x] bar\n- [ ] \\(Optional) baz\n- [ ] 💣{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsATable() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddTable(fixture.TestCase.Table).WriteAsync();

        var expected = $"<table><tr><th>foo</th><th>bar</th><th>baz</th><td rowspan=\"3\">tall</td></tr><tr><td>one</td><td>two</td><td>three</td></tr><tr><td colspan=\"3\">wide</td></tr></table>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownTable() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownTable(fixture.TestCase.SummaryTable).WriteAsync();

        var expected = $"| foo | bar | baz |\n| --: | --- | :-- |\n| one | two | 333 |\n| a | b | c |{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsADetailsElement() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut
          .AddDetails(fixture.TestCase.Details.Label, fixture.TestCase.Details.Content)
          .WriteAsync();

        var expected = $"<details><summary>open me</summary>🎉 surprise</details>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsAnImageWithAltText() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddImage(fixture.TestCase.Img.Src, fixture.TestCase.Img.Alt).WriteAsync();

        var expected = $"<img src=\"https://github.com/actions.png\" alt=\"actions logo\">{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsAnImageWithCustomDimensions() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut
          .AddImage(fixture.TestCase.Img.Src, fixture.TestCase.Img.Alt, fixture.TestCase.Img.Options)
          .WriteAsync();

        var expected = $"<img src=\"https://github.com/actions.png\" alt=\"actions logo\" width=\"32\" height=\"32\">{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsHeadingsH1ToH6() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();
        for (var i = 1; i <= 6; i++)
        {
            sut.AddHeading("heading", i);
        }

        await sut.WriteAsync();

        var expected = $"<h1>heading</h1>{Environment.NewLine}<h2>heading</h2>{Environment.NewLine}<h3>heading</h3>{Environment.NewLine}<h4>heading</h4>{Environment.NewLine}<h5>heading</h5>{Environment.NewLine}<h6>heading</h6>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownHeadingsH1ToH6() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();
        for (var i = 1; i <= 6; i++)
        {
            sut.AddMarkdownHeading("heading", i);
        }

        await sut.WriteAsync();

        var expected = $"# heading{Environment.NewLine}## heading{Environment.NewLine}### heading{Environment.NewLine}#### heading{Environment.NewLine}##### heading{Environment.NewLine}###### heading{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsH1IfHeadingLevelNotSpecified() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddHeading("heading").WriteAsync();

        var expected = $"<h1>heading</h1>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownH1IfHeadingLevelNotSpecified() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownHeading("heading").WriteAsync();

        var expected = $"# heading{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task UsesH1IfHeadingLevelIsGarbageOrOutOfRange() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddHeading("heading", 1337)
          .AddHeading("heading", -1)
          .WriteAsync();

        var expected = $"<h1>heading</h1>{Environment.NewLine}<h1>heading</h1>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task UsesMarkdownH1IfHeadingLevelIsGarbageOrOutOfRange() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownHeading("heading", 1337)
          .AddMarkdownHeading("heading", -1)
          .WriteAsync();

        var expected = $"# heading{Environment.NewLine}# heading{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsASeparator() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddSeparator().WriteAsync();

        var expected = $"<hr>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownSeparator() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownSeparator().WriteAsync();

        var expected = $"---{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsABreak() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddBreak().WriteAsync();

        var expected = $"<br>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsAQuote() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddQuote(fixture.TestCase.Quote.Text).WriteAsync();

        var expected = $"<blockquote>Where the world builds software</blockquote>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownQuote() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownQuote(fixture.TestCase.Quote.Text).WriteAsync();

        var expected = $"> Where the world builds software{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsAQuoteWithCitation() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddQuote(fixture.TestCase.Quote.Text, fixture.TestCase.Quote.Cite).WriteAsync();

        var expected = $"<blockquote cite=\"https://github.com/about\">Where the world builds software</blockquote>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsALinkWithHref() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddLink(fixture.TestCase.Link.Text, fixture.TestCase.Link.Href).WriteAsync();

        var expected = $"<a href=\"https://github.com\">GitHub</a>{Environment.NewLine}";

        await AssertSummary(expected);
    });

    [Fact]
    public Task AddsMarkdownLinkWithHref() => fixture.TestAsync(async () =>
    {
        var sut = new Summary();

        await sut.AddMarkdownLink(fixture.TestCase.Link.Text, fixture.TestCase.Link.Href).WriteAsync();

        var expected = $"[GitHub](https://github.com){Environment.NewLine}";

        await AssertSummary(expected);
    });
}
