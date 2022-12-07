﻿// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit.Common;

/// <summary>
/// Represents a GitHub repository.
/// </summary>
/// <param name="Owner">The owner of the issue. The first segment of this URL: <c>dotnet/runtime</c>, the owner would be <c>dotnet</c>.</param>
/// <param name="Repo">The repo of the issue. The second segment of this URL: <c>dotnet/runtime</c>, the owner would be <c>runtime</c>.</param>
public readonly record struct Repository(
    string Owner,
    string Repo);
