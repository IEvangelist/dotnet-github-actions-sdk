// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.IO.Tests;

public sealed class TempFolderTestFixture : IDisposable
{
    public TempFolderTestFixture() =>
        TempFolder =
            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()))
                .FullName;

    internal string TempFolder { get; }

    void IDisposable.Dispose() => Directory.Delete(TempFolder, true);
}