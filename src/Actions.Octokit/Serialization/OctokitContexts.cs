// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using PR = Actions.Octokit.Interfaces.PullRequest;
using Install = Actions.Octokit.Interfaces.Installation;

namespace Actions.Octokit.Serialization;

[JsonSerializable(typeof(WebhookPayload))]
[JsonSerializable(typeof(PayloadRepository))]
[JsonSerializable(typeof(WebhookIssue))]
[JsonSerializable(typeof(PR))]
[JsonSerializable(typeof(Sender))]
[JsonSerializable(typeof(Install))]
[JsonSerializable(typeof(Comment))]
internal partial class OctokitContexts : JsonSerializerContext
{
}
