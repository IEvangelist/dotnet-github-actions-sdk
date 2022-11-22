// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Commands;

public interface IFileCommandIssuer
{
    Task IssueFileCommandAsync<TValue>(string commandSuffix, TValue message);

    string PrepareKeyValueMessage<TValue>(string key, TValue value);
}
