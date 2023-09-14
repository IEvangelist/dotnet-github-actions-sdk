// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Core.Summaries;

/// <summary>
/// Represents a row in a summary table.
/// </summary>
/// <param name="Data">Cell content</param>
/// <param name="Header">Render cell as header. (optional) default: false</param>
/// <param name="Colspan">Number of columns the cell extends. (optional) default: "1"</param>
/// <param name="Rowspan">Number of rows the cell extends. (optional) default: "1"</param>
public readonly record struct SummaryTableCell(
    string Data,
    bool? Header = null,
    string? Colspan = "1",
    string? Rowspan = "1")
{
    /// <summary>
    /// Whether or not the cell is considered simple, meaning 
    /// only the <see cref="Data" /> is provided.
    /// </summary>
    public bool IsSimpleCell => Header is null && Colspan is "1" && Rowspan is "1";
}
