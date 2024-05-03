// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.Core.EnvironmentVariables;

/// <summary>
/// A collection of environment variable keys, commonly used by GitHub Actions.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Naming",
    "CA1707:Identifiers should not contain underscores",
    Justification = "These values correspond to environment variables and I want them to match exactly.")]
public static class Keys
{
    /// <summary>
    /// The environment variable key: <c>GITHUB_ENV</c>.
    /// </summary>
    public const string GITHUB_ENV = nameof(GITHUB_ENV);

    /// <summary>
    /// The environment variable key: <c>GITHUB_OUTPUT</c>.
    /// </summary>
    public const string GITHUB_OUTPUT = nameof(GITHUB_OUTPUT);

    /// <summary>
    /// The environment variable key: <c>GITHUB_PATH</c>.
    /// </summary>
    public const string GITHUB_PATH = nameof(GITHUB_PATH);

    /// <summary>
    /// The environment variable key: <c>GITHUB_STATE</c>.
    /// </summary>
    public const string GITHUB_STATE = nameof(GITHUB_STATE);

    /// <summary>
    /// The environment variable key: <c>GITHUB_STEP_SUMMARY</c>.
    /// </summary>
    public const string GITHUB_STEP_SUMMARY = nameof(GITHUB_STEP_SUMMARY);

    /// <summary>
    /// The environment variable key: <c>GITHUB_TOKEN</c>.
    /// </summary>
    public const string GITHUB_TOKEN = nameof(GITHUB_TOKEN);

    /// <summary>
    /// The environment variable key: <c>RUNNER_DEBUG</c>.
    /// </summary>
    public const string RUNNER_DEBUG = nameof(RUNNER_DEBUG);

    /// <summary>
    /// The environment variable key: <c>PATH</c>.
    /// </summary>
    public const string PATH = nameof(PATH);

    /// <summary>
    /// The environment variable key: <c>GITHUB_WORKSPACE</c>.
    /// </summary>
    public const string GITHUB_WORKSPACE = nameof(GITHUB_WORKSPACE);

    /// <summary>
    /// The environment variable key: <c>ACTIONS_STEP_DEBUG</c>.
    /// </summary>
    public const string ACTIONS_STEP_DEBUG = nameof(ACTIONS_STEP_DEBUG);
}
