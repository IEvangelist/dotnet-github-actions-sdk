// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Commands;

internal static class CommandConstants
{
    public static readonly string SetEnv = "set-env";
    public static readonly string AddMask = "add-mask";
    public static readonly string AddPath = "add-path";
    public static readonly string Echo = "echo";
    public static readonly string Debug = "debug";
    public static readonly string Error = "error";
    public static readonly string Warning = "warning";
    public static readonly string Notice = "notice";
    public static readonly string Group = "group";
    public static readonly string EndGroup = "endgroup";

    // Deprecated
    public static readonly string SaveState = "save-state";
    public static readonly string SetOutput = "set-output";
}
