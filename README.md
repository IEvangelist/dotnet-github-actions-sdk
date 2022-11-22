# GitHub Actions Workflow .NET SDK 

[![build-and-test](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/build-and-test.yml)

The .NET equivalent of the official GitHub [actions/toolkit](https://github.com/actions/toolkit) `@actions/core` project.

## Usage

To use the `IWorkflowStepService` in your .NET project, you can install the `GitHub.Actions` NuGet package. From an `IServiceCollection` instance, call `AddGitHubActions` and then your consuming code can require the `IWorkflowStepService` via constructor dependency injection.
