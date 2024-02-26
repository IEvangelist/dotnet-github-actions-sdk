// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.HttpClient;

internal static class ClientNames
{
    /// <summary>Basic username and password.</summary>
    internal const string Basic = nameof(Basic);

    /// <summary>Personal access token (PAT)</summary>
    internal const string Pat = nameof(Pat);

    /// <summary>Bearer token.</summary>
    internal const string Bearer = nameof(Bearer);
}
