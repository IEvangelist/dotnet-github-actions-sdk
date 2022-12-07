// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.IO;

/// <summary>
/// Options object for move <c>mv</c>.
/// </summary>
/// <param name="Force">
/// Whether to overwrite existing files in the destination.
/// Defaults to <c>true</c>.
/// </param>
public readonly record struct MoveOptions(
    bool Force = true);
