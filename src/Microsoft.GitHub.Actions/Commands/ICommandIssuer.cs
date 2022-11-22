// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Commands;

public interface ICommandIssuer
{
    void IssueCommand<T>(
        string command,
        IDictionary<string, string>? properties = default,
        T? message = default);

    void Issue<T>(string name, T? message = default);
}
