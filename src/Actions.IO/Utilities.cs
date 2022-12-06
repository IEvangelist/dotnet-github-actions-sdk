// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.IO;

public static class Utilities
{
    public static bool Exists(string path) => File.Exists(path);

    public static bool IsRooted(string path) => Path.IsPathRooted(path);
}
