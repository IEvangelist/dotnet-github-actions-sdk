# GitHub Actions Workflow .NET SDK

[![build-and-test](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/build-and-test.yml)
[![code analysis](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/codeql-analysis.yml)
[![publish nuget](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/publish-nuget.yml/badge.svg)](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/GitHub.Actions.Core.svg?style=flat)](https://www.nuget.org/packages/GitHub.Actions.Core)

The .NET equivalent of the official GitHub [actions/toolkit](https://github.com/actions/toolkit) repository, and is currently a work in progress. While there isn't currently 100% feature complete compatibility between these two repositories, that is the eventual goal.

## Blog

[🔗 Hello from the GitHub Actions: Core .NET SDK](https://davidpine.net/blog/github-actions-sdk)

## GitHub Actions .NET Toolkit

The GitHub Actions .NET ToolKit provides a set of packages to make creating actions easier.

## Packages

:heavy_check_mark: [`GitHub.Actions.Core`](src/Actions.Core)

Provides functions for inputs, outputs, results, logging, secrets and variables. Read more [here](src/Actions.Core)

```
dotnet add package GitHub.Actions.Core
```

For more information, see [📦 GitHub.Actions.Core](https://www.nuget.org/packages/GitHub.Actions.Core).

:ice_cream: [`GitHub.Actions.Glob`](src/Actions.Glob)

Provides functions to search for files matching glob patterns. Read more [here](src/Actions.Glob)

```
dotnet add package GitHub.Actions.Glob
```

For more information, see [📦 GitHub.Actions.Glob](https://www.nuget.org/packages/GitHub.Actions.Glob).

<!--

:pencil2: [`GitHub.Actions.IO`](src/Actions.IO)

Provides disk i/o functions like cp, mv, rmRF, which etc. Read more [here](src/Actions.IO)

```
dotnet add package GitHub.Actions.IO
```

For more information, see [📦 GitHub.Actions.IO](https://www.nuget.org/packages/GitHub.Actions.IO).

-->

:octocat: [`GitHub.Actions.Octokit`](src/Actions.Octokit)

Provides an Octokit client hydrated with the context that the current action is being run in. Read more [here](src/Actions.Octokit)

```bash
dotnet add package GitHub.Actions.Octokit
```

For more information, see [📦 GitHub.Actions.Octokit](https://www.nuget.org/packages/GitHub.Actions.Octokit).