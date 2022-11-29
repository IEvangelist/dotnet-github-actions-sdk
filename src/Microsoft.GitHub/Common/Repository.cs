// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Common;

public readonly record struct Repository(
    string Owner,
    string Repo);
