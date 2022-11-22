// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.EnvironmentVariables;

/// <summary>
/// A collection of environment variable keys, commonly used by GitHub Actions.
/// </summary>
public static class Keys
{
    // GitHub prefixed env vars.
    public const string GITHUB_ACTION = nameof(GITHUB_ACTION);
    public const string GITHUB_ACTOR = nameof(GITHUB_ACTOR);
    public const string GITHUB_BASE_REF = nameof(GITHUB_BASE_REF);
    public const string GITHUB_ENV = nameof(GITHUB_ENV);
    public const string GITHUB_EVENT_NAME = nameof(GITHUB_EVENT_NAME);
    public const string GITHUB_EVENT_PATH = nameof(GITHUB_EVENT_PATH);
    public const string GITHUB_HEAD_REF = nameof(GITHUB_HEAD_REF);
    public const string GITHUB_OUTPUT = nameof(GITHUB_OUTPUT);
    public const string GITHUB_PATH = nameof(GITHUB_PATH);
    public const string GITHUB_REF = nameof(GITHUB_REF);
    public const string GITHUB_REPOSITORY = nameof(GITHUB_REPOSITORY);
    public const string GITHUB_SERVER_URL = nameof(GITHUB_SERVER_URL);
    public const string GITHUB_SHA = nameof(GITHUB_SHA);
    public const string GITHUB_STATE = nameof(GITHUB_STATE);
    public const string GITHUB_STEP_SUMMARY = nameof(GITHUB_STEP_SUMMARY);
    public const string GITHUB_TOKEN = nameof(GITHUB_TOKEN);
    public const string GITHUB_WORKFLOW = nameof(GITHUB_WORKFLOW);
    public const string GITHUB_WORKSPACE = nameof(GITHUB_WORKSPACE);

    // Globals
    public const string RUNNER_DEBUG = nameof(RUNNER_DEBUG);
    public const string PATH = nameof(PATH);

    // Action step env vars.
    public const string ACTIONS_STEP_DEBUG = nameof(ACTIONS_STEP_DEBUG);
}
