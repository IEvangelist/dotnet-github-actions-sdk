// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics;

namespace Microsoft.GitHub.Actions.Extensions;

internal static class ProcessExtensions
{
    internal static void GracefullOrForcedShutdown(this int pid) =>
        Process.GetProcessById(pid)
            ?.GracefullOrForcedShutdown();

    internal static void GracefullOrForcedShutdown(this Process process)
    {
        if (process is null)
        {
            return;
        }

        if (process.CloseMainWindow() is false)
        {
            process.WaitForExit(TimeSpan.FromSeconds(3));
        }

        while (process is not { HasExited: true })
        {
            process.Kill(true);
        }
    }
}
