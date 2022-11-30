// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Summaries;

public sealed class Summary
{
    public StringBuilder _buffer = new();
    public string? _filePath;

    /// <summary>If the summary buffer is empty</summary>
    /// <returns><c>true</c> if the buffer is empty</returns>
    public bool IsEmptyBuffer => _buffer.Length is 0;

    /// <summary>Finds the summary file path from the environment, rejects if env var 
    /// is not found or file does not exist. Also checks r/w permissions.</summary>
    /// <returns>The step summary file path</returns>
    private string FilePath()
    {
        if (_filePath is not null)
        {
            return _filePath;
        }

        var pathFromEnv = GetEnvironmentVariable(GITHUB_STEP_SUMMARY);
        if (string.IsNullOrWhiteSpace(pathFromEnv))
        {
            throw new Exception($"""
                Unable to find environment variable for {GITHUB_STEP_SUMMARY}. 
                Check if your runtime environment supports job summaries.
                """);
        }

        try
        {
            using var fs = new FileStream(pathFromEnv, FileMode.Open);
            if (fs is not { CanRead: true } and not { CanWrite: true })
            {
                Throw(pathFromEnv);
            }
        }
        catch (Exception ex)
        {
            Throw(pathFromEnv, ex);
        }

        return _filePath = pathFromEnv;

        static void Throw(string path, Exception? exception = null)
        {
            throw new Exception(
                $"Unable to access summary file: '{path}'. Check if the file has correct read/write permissions.",
                exception);
        }
    }

    /// <summary>Wraps content in an HTML tag, adding any HTML attributes</summary>
    /// <param name="tag">tag HTML tag to wrap</param>
    /// <param name="content">content content within the tag</param>
    /// <param name="attributes">attrs key-value list of HTML attributes to add</param>
    /// <returns>A <c>string</c> content wrapped in HTML element</returns>
    private static string Wrap(
        string tag,
        string? content,
        IReadOnlyDictionary<string, string>? attributes = null)
    {
        var htmlAttrs = attributes?.Select(
            kvp => $"{kvp.Key}={kvp.Value}");

        return content is null or { Length: 0 }
            ? $"<{tag} {htmlAttrs}>"
            : $"<{tag} {htmlAttrs}>{content}</{tag}>";
    }

    /// <summary>Writes text in the buffer to the summary buffer file and empties buffer. Will append by default.</summary>
    /// <param name="options">The (optional) <see cref="SummaryWriteOptions"/> for write operation.</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public async Task<Summary> WriteAsync(SummaryWriteOptions? options)
    {
        var filePath = FilePath();

        var summaryContents = _buffer.ToString();
        Func<string, string?, Encoding, CancellationToken, Task> writeTask = options is { Overwrite: true }
            ? File.WriteAllTextAsync
            : File.AppendAllTextAsync;

        await writeTask(filePath, summaryContents, Encoding.UTF8, default);

        return EmptyBuffer();
    }

    /// <summary>Clears the summary buffer and wipes the summary file</summary>
    /// <returns>The <c>Summary</c> instance</returns>
    public Task<Summary> ClearAsync() =>
        EmptyBuffer().WriteAsync(new(true));

    /// <summary>Returns the current summary buffer as a string</summary>
    /// <returns>A <c>string</c> representation of the summary buffer</returns>
    public string Stringify() => _buffer.ToString();

    /// <summary>Resets the summary buffer without writing to summary file</summary>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary EmptyBuffer()
    {
        _buffer.Clear();
        return this;
    }

    /// <summary>Adds raw text to the summary buffer</summary>
    /// <param name="text">text content to add</param>
    /// <param name="addNewLine">(optional) append a <see cref="NewLine"/> to the raw text (default: false)</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddRaw(string text, bool addNewLine = false)
    {
        _buffer.Append(text);
        return addNewLine ? AddNewLine() : this;
    }

    /// <summary>Adds the operating system-specific end-of-line marker to the buffer</summary>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddNewLine() => AddRaw(NewLine);

    /// <summary>Adds an HTML codeblock to the summary buffer</summary>
    /// <param name="code">code content to render within fenced code block</param>
    /// <param name="lang">lang (optional) language to syntax highlight code</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddCodeBlock(string code, string? lang = null)
    {
        var attrs = lang is not null
            ? new Dictionary<string, string> { [nameof(lang)] = lang }
            : null;
        var element = Wrap("pre", Wrap("code", code), attrs);

        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds an HTML list to the summary buffer</summary>
    /// <param name="items">items list of items to render</param>
    /// <param name="ordered">(optional) if the rendered list should be ordered or not (default: false)</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddList(string[] items, bool ordered = false)
    {
        var tag = ordered ? "ol" : "ul";
        var listItems = string.Join("", items.Select(item => Wrap("li", item)));
        var element = Wrap(tag, listItems);

        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds an HTML table to the summary buffer</summary>
    /// <param name="rows">rows table rows</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddTable(SummaryTableRow[] rows)
    {
        var tableBody = string.Join("", rows.Select(
            row =>
            {
                var cells = string.Join("", row.Cells.Select(
                    cell =>
                    {
                        if (cell.IsSimpleCell)
                        {
                            return Wrap("td", cell.Data);
                        }

                        var (data, header, colspan, rowspan) = cell;
                        var tag = header is true ? "th" : "td";
                        var attrs = colspan is not null && rowspan is not null
                            ? new Dictionary<string, string>
                            {
                                [nameof(colspan)] = colspan,
                                [nameof(rowspan)] = rowspan,
                            }
                            : null;

                        return Wrap(tag, data, attrs);
                    }));

                return Wrap("tr", cells);
            }));

        var element = Wrap("table", tableBody);
        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds a collapsable HTML details element to the summary buffer</summary>
    /// <param name="label">label text for the closed state</param>
    /// <param name="content">content collapsable content</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddDetails(string label, string content)
    {
        var element = Wrap("details", $"{Wrap("summary", label)}{content}");
        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds an HTML image tag to the summary buffer</summary>
    /// <param name="src">src path to the image you to embed</param>
    /// <param name="alt">alt text description of the image</param>
    /// <param name="options">(optional) addition image attributes</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddImage(string src, string alt, SummaryImageOptions? options = default)
    {
        var attrs = new Dictionary<string, string>
        {
            [nameof(src)] = src,
            [nameof(alt)] = alt
        };

        var (width, height) = options.GetValueOrDefault();
        if (width is not null)
        {
            attrs[nameof(width)] = width;
        }
        if (height is not null)
        {
            attrs[nameof(height)] = height;
        }

        var element = Wrap("img", null, attrs);
        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds an HTML section heading element</summary>
    /// <param name="text">text heading text</param>
    /// <param name="level">(optional) the heading level, default: 1</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddHeading(string text, int? level)
    {
        var tag = $"h{level}";
        var allowedTag = new[] { "h1", "h2", "h3", "h4", "h5", "h6" }.Any(h => h == tag)
            ? tag : "h1";
        var element = Wrap(allowedTag, text);
        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds an HTML thematic break (<c>&lt;hr&gt;</c>) to the summary buffer</summary>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddSeparator()
    {
        var element = Wrap("hr", null);
        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds an HTML line break (<c>&lt;br&gt;</c>) to the summary buffer</summary>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddBreak()
    {
        var element = Wrap("br", null);
        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds an HTML blockquote to the summary buffer</summary>
    /// <param name="text">quote text</param>
    /// <param name="cite">cite (optional) citation url</param>
    /// <param name="attributes">attrs key-value list of HTML attributes to add</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddQuote(string text, string? cite = null)
    {
        var attrs = cite is not null
            ? new Dictionary<string, string>
            {
                [nameof(cite)] = cite
            }
            : null;
        var element = Wrap("blockquote", text, attrs);
        return AddRaw(element).AddNewLine();
    }

    /// <summary>Adds an HTML anchor tag to the summary buffer</summary>
    /// <param name="text">text link text/content</param>
    /// <param name="href">href hyperlink</param>
    /// <returns>The <c>Summary</c> instance</returns>
    public Summary AddLink(string text, string href)
    {
        var element = Wrap("a", text, new Dictionary<string, string>() { [nameof(href)] = href });
        return AddRaw(element).AddNewLine();
    }
}