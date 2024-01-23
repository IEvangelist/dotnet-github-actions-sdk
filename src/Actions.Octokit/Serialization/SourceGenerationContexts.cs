// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using CommonIssue = Actions.Octokit.Common.Issue;
using CommonRepo = Actions.Octokit.Common.Repository;

using PR = Actions.Octokit.Interfaces.PullRequest;
using Install = Actions.Octokit.Interfaces.Installation;

namespace Actions.Octokit.Serialization;

[JsonSerializable(typeof(CommonIssue))]
[JsonSerializable(typeof(CommonRepo))]
[JsonSerializable(typeof(Comment))]
[JsonSerializable(typeof(Install))]
[JsonSerializable(typeof(Owner))]
[JsonSerializable(typeof(PayloadRepository))]
[JsonSerializable(typeof(PR))]
[JsonSerializable(typeof(Sender))]
[JsonSerializable(typeof(WebhookIssue))]
[JsonSerializable(typeof(WebhookPayload))]
internal partial class SourceGenerationContexts : JsonSerializerContext
{
}
