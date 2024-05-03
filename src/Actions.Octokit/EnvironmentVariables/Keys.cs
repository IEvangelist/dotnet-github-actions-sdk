// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Octokit.EnvironmentVariables;

/// <summary>
/// A collection of environment variable keys, commonly used by GitHub Actions.
/// </summary>
[SuppressMessage(
    "Naming",
    "CA1707:Identifiers should not contain underscores",
    Justification = "These values correspond to environment variables and I want them to match exactly.")]
public static class Keys
{
    /// <summary>
    /// The environment variable key: <c>GITHUB_ACTION</c>.
    /// </summary>
    public const string GITHUB_ACTION = nameof(GITHUB_ACTION);

    /// <summary>
    /// The environment variable key: <c>GITHUB_ACTOR</c>.
    /// </summary>
    public const string GITHUB_ACTOR = nameof(GITHUB_ACTOR);

    /// <summary>
    /// The environment variable key: <c>GITHUB_JOB</c>.
    /// </summary>
    public const string GITHUB_JOB = nameof(GITHUB_JOB);

    /// <summary>
    /// The environment variable key: <c>GITHUB_RUN_ATTEMPT</c>.
    /// </summary>
    public const string GITHUB_RUN_ATTEMPT = nameof(GITHUB_RUN_ATTEMPT);

    /// <summary>
    /// The environment variable key: <c>GITHUB_RUN_NUMBER</c>.
    /// </summary>
    public const string GITHUB_RUN_NUMBER = nameof(GITHUB_RUN_NUMBER);
    /// <summary>
    /// The environment variable key: <c>GITHUB_RUN_ID</c>.
    /// </summary>
    public const string GITHUB_RUN_ID = nameof(GITHUB_RUN_ID);

    /// <summary>
    /// The environment variable key: <c>GITHUB_API_URL</c>.
    /// </summary>
    public const string GITHUB_API_URL = nameof(GITHUB_API_URL);

    /// <summary>
    /// The environment variable key: <c>GITHUB_SERVER_URL</c>.
    /// </summary>
    public const string GITHUB_SERVER_URL = nameof(GITHUB_SERVER_URL);

    /// <summary>
    /// The environment variable key: <c>GITHUB_GRAPHQL_URL</c>.
    /// </summary>
    public const string GITHUB_GRAPHQL_URL = nameof(GITHUB_GRAPHQL_URL);

    /// <summary>
    /// The environment variable key: <c>GITHUB_BASE_REF</c>.
    /// </summary>
    public const string GITHUB_BASE_REF = nameof(GITHUB_BASE_REF);

    /// <summary>
    /// The environment variable key: <c>GITHUB_EVENT_NAME</c>.
    /// </summary>
    public const string GITHUB_EVENT_NAME = nameof(GITHUB_EVENT_NAME);

    /// <summary>
    /// The environment variable key: <c>GITHUB_EVENT_PATH</c>.
    /// </summary>
    public const string GITHUB_EVENT_PATH = nameof(GITHUB_EVENT_PATH);

    /// <summary>
    /// The environment variable key: <c>GITHUB_HEAD_REF</c>.
    /// </summary>
    public const string GITHUB_HEAD_REF = nameof(GITHUB_HEAD_REF);

    /// <summary>
    /// The environment variable key: <c>GITHUB_ACTION</c>.
    /// </summary>
    public const string GITHUB_REF = nameof(GITHUB_REF);

    /// <summary>
    /// The environment variable key: <c>GITHUB_REPOSITORY</c>.
    /// </summary>
    public const string GITHUB_REPOSITORY = nameof(GITHUB_REPOSITORY);

    /// <summary>
    /// The environment variable key: <c>GITHUB_SHA</c>.
    /// </summary>
    public const string GITHUB_SHA = nameof(GITHUB_SHA);

    /// <summary>
    /// The environment variable key: <c>GITHUB_WORKFLOW</c>.
    /// </summary>
    public const string GITHUB_WORKFLOW = nameof(GITHUB_WORKFLOW);
}
