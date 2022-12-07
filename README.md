# GitHub Actions Workflow .NET SDK

[![build-and-test](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/build-and-test.yml)
[![code analysis](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/codeql-analysis.yml)
[![publish nuget](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/publish-nuget.yml/badge.svg)](https://github.com/IEvangelist/dotnet-github-actions-sdk/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/GitHub.Actions.Core.svg?style=flat)](https://www.nuget.org/packages/GitHub.Actions.Core)

The .NET equivalent of the official GitHub [actions/toolkit](https://github.com/actions/toolkit) `@actions/core` project.

## Blog

[🔗 Hello from the GitHub Actions: Core .NET SDK](https://davidpine.net/blog/github-actions-sdk)

## Usage

### Installing the NuGet package 📦

Welcome to the [Actions.Core] .NET SDK. This SDK is used to create GitHub Actions in .NET. The SDK is a thin wrapper around the .NET implementation of the GitHub Actions a select few packages from the [`@actions/toolkit`](https://github.com/actions/toolkit).

> **Warning**:
> This package is **not** an official _Microsoft_ or _GitHub_ product. It is a community-driven project. However, I do choose to use the `Actions.Octokit[.*]` namespace for the package. I'm not trying to mislead anyone, but I do want to make it clear that this is not an official product.

You'll need to install the [GitHub Actions Workflow .NET SDK](https://www.nuget.org/packages/GitHub.Actions.Core) NuGet package to use the .NET APIs. The package is available on NuGet.org. The following is the command to install the package:

#### Adding package references

Either add the package reference to your project file:

```xml
<PackageReference Include="GitHub.Actions.Core" Version="1.0.0" />
```

Or use the [`dotnet add package`](https://learn.microsoft.com/dotnet/core/tools/dotnet-add-package) .NET CLI command:

```bash
dotnet add package GitHub.Actions.Core
```

### Get the `ICoreService` instance

To use the `ICoreService` in your .NET project, register the services with an `IServiceCollection` instance by calling `AddGitHubActions` and then your consuming code can require the `ICoreService` via constructor dependency injection.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Actions.Core;
using Actions.Core.Extensions;

using var provider = new ServiceCollection()
    .AddGitHubActions()
    .BuildServiceProvider();

var core = provider.GetRequiredService<ICoreService>();
```

## `Actions.Core`

This was modified, but borrowed from the [_core/README.md_](https://github.com/actions/toolkit/blob/main/packages/core/README.md).

> Core functions for setting results, logging, registering secrets and exporting variables across actions

### Using declarations

```csharp
global using Actions.Core;
```

#### Inputs/Outputs

Action inputs can be read with `GetInput` which returns a `string` or `GetBoolInput` which parses a `bool` based on the [yaml 1.2 specification](https://yaml.org/spec/1.2/spec.html#id2804923). If `required` is `false`, the input should have a default value in `action.yml`.

Outputs can be set with `SetOutputAsync` which makes them available to be mapped into inputs of other actions to ensure they are decoupled.

```csharp
var myInput = core.GetInput("inputName", new InputOptions(true));
var myBoolInput = core.GetBoolInput("boolInputName", new InputOptions(true));
var myMultilineInput = core.GetMultilineInput("multilineInputName", new InputOptions(true));
await core.SetOutputAsync("outputKey", "outputVal");
```

#### Exporting variables

Since each step runs in a separate process, you can use `ExportVariableAsync` to add it to this step and future steps environment blocks.

```csharp
await core.ExportVariableAsync("envVar", "Val");
```

#### Setting a secret

Setting a secret registers the secret with the runner to ensure it is masked in logs.

```csharp
core.SetSecret("myPassword");
```

#### PATH Manipulation

To make a tool's path available in the path for the remainder of the job (without altering the machine or containers state), use `AddPathAsync`.  The runner will prepend the path given to the jobs PATH.

```csharp
await core.AddPathAsync("/path/to/mytool");
```

#### Exit codes

You should use this library to set the failing exit code for your action.  If status is not set and the script runs to completion, that will lead to a success.

```csharp
using var provider = new ServiceCollection()
    .AddGitHubActions()
    .BuildServiceProvider();

var core = provider.GetRequiredService<ICoreService>();

try 
{
    // Do stuff
}
catch (Exception ex)
{
  // SetFailed logs the message and sets a failing exit code
  core.SetFailed($"Action failed with error {ex}"");
}
```

#### Logging

Finally, this library provides some utilities for logging. Note that debug logging is hidden from the logs by default. This behavior can be toggled by enabling the [Step Debug Logs](../../docs/action-debugging.md#step-debug-logs).

```csharp
using var provider = new ServiceCollection()
    .AddGitHubActions()
    .BuildServiceProvider();

var core = provider.GetRequiredService<ICoreService>();

var myInput = core.GetInput("input");
try
{
    core.Debug("Inside try block");
    
    if (!myInput)
    {
        core.Warning("myInput was not set");
    }
    
    if (core.IsDebug)
    {
        // curl -v https://github.com
    }
    else
    {
        // curl https://github.com
    }
    
    // Do stuff
    core.Info("Output to the actions build log");
    
    core.Notice("This is a message that will also emit an annotation");
}
catch (Exception ex)
{
    core.Error($"Error {ex}, action may still succeed though");
}
```

This library can also wrap chunks of output in foldable groups.

```csharp
using var provider = new ServiceCollection()
    .AddGitHubActions()
    .BuildServiceProvider();

var core = provider.GetRequiredService<ICoreService>();

// Manually wrap output
core.StartGroup("Do some function");
SomeFunction();
core.EndGroup();

// Wrap an asynchronous function call
var result = await core.GroupAsync("Do something async", async () =>
{
    var response = await MakeHttpRequestAsync();
    return response
});
```

#### Styling output

Colored output is supported in the Action logs via standard [ANSI escape codes](https://en.wikipedia.org/wiki/ANSI_escape_code). 3/4 bit, 8 bit and 24 bit colors are all supported.

Foreground colors:

```csharp
// 3/4 bit
core.Info("\u001b[35mThis foreground will be magenta");

// 8 bit
core.Info("\u001b[38;5;6mThis foreground will be cyan");

// 24 bit
core.Info("\u001b[38;2;255;0;0mThis foreground will be bright red");
```

Background colors:

```csharp
// 3/4 bit
core.Info("\u001b[43mThis background will be yellow");

// 8 bit
core.Info("\u001b[48;5;6mThis background will be cyan");

// 24 bit
core.Info("\u001b[48;2;255;0;0mThis background will be bright red");
```

Special styles:

```csharp
core.Info("\u001b[1mBold text");
core.Info("\u001b[3mItalic text");
core.Info("\u001b[4mUnderlined text");
```

ANSI escape codes can be combined with one another:

```csharp
core.Info("\u001b[31;46mRed foreground with a cyan background and \u001b[1mbold text at the end");
```

> Note: Escape codes reset at the start of each line.

```csharp
core.Info("\u001b[35mThis foreground will be magenta");
core.Info("This foreground will reset to the default");
```
