# GitHub `Actions.Glob` .NET SDK

The .NET equivalent of the official GitHub [actions/toolkit](https://github.com/actions/toolkit) `@actions/glob` project.

## Blog

[🔗 Hello from the GitHub `Actions.Glob` .NET SDK](https://davidpine.net/blog/github-actions-sdk)

## Usage

### Installing the NuGet package 📦

Welcome to the `Actions.Glob` .NET SDK. This SDK is used to create GitHub Actions in .NET. The SDK is a thin wrapper around the .NET implementation of the GitHub Actions a select few packages from the [`@actions/toolkit`](https://github.com/actions/toolkit).

> **Warning**:
> This package is **not** an official _Microsoft_ or _GitHub_ product. It is a community-driven project.

You"ll need to install the [GitHub `Actions.Glob` .NET SDK](https://www.nuget.org/packages/Actions.Glob) NuGet package to use the .NET APIs. The package is available on NuGet.org. The following is the command to install the package:

#### Adding package references

Either add the package reference to your project file:

```xml
<PackageReference Include="GitHub.Actions.Glob" Version="0.0.1" />
```

Or use the [`dotnet add package`](https://learn.microsoft.com/dotnet/core/tools/dotnet-add-package) .NET CLI command:

```bash
dotnet add package Actions.Glob
```

### Get the `IGlobPatternResolverBuilder` instance

To use the `IGlobPatternResolverBuilder` in your .NET project, register the services with an `IServiceCollection` instance by calling `AddGitHubActionsGlob` and then your consuming code can require the `IGlobPatternResolverBuilder` via constructor dependency injection.

```csharp
using Microsoft.Extensions.DependencyInjection;
using Actions.Glob;
using Actions.Glob.Extensions;

using var provider = new ServiceCollection()
    .AddGitHubActionsGlob()
    .BuildServiceProvider();

var glob = provider.GetRequiredService<IGlobPatternResolverBuilder>();
```

## `Actions.Glob`

This was modified, but borrowed from the [_glob/README.md_](https://github.com/actions/toolkit/blob/main/packages/glob/README.md).

> You can use this package to search for files matching glob patterns.

### Basic usage

Relative paths and absolute paths are both allowed. Relative paths are rooted against the current working directory.

```csharp
using Actions.Glob;
using Actions.Glob.Extensions;

var patterns = new[] { "**/tar.gz", "**/tar.bz" };
var globber = Globber.Create(patterns);
var files = globber.GlobFiles();
```

### Get all files recursively

```csharp
using Actions.Glob;
using Actions.Glob.Extensions;

var globber = Globber.Create("**/*");
var files = globber.GlobFiles();
```

### Iterating files

When dealing with a large amount of results, consider iterating the results as they are returned:

```csharp
using Actions.Glob;
using Actions.Glob.Extensions;

var globber = Globber.Create("**/*");
foreach (var file in globber.GlobFiles())
{
    // Do something with the file
}
```

## Recommended action inputs

When an action allows a user to specify input patterns, it is generally recommended to
allow users to opt-out from following symbolic links.

Snippet from `action.yml`:

```yaml
inputs:
  files:
    description: "Files to print"
    required: true
```

And corresponding toolkit consumption:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Actions.Core;
using Actions.Core.Extensions;
using Actions.Glob;
using Actions.Glob.Extensions;

using var provider = new ServiceCollection()
    .AddGitHubActionsCore()
    .BuildServiceProvider();

var core = provider.GetRequiredService<ICoreService>();

var globber = Globber.Create(core.GetInput("files"))
foreach (var file in globber.GlobFiles())
{
    // Do something with the file
}
```

## Pattern formats

The patterns that are specified in the `AddExclude` and `AddInclude` methods can use the following formats to match multiple files or directories.

- Exact directory or file name
  
  - `some-file.txt`
  - `path/to/file.txt`

- Wildcards `*` in file and directory names that represent zero to many characters not including separator characters.

    | Value          | Description                                                            |
    |----------------|------------------------------------------------------------------------|
    | `*.txt`        | All files with *.txt* file extension.                                  |
    | `*.*`          | All files with an extension.                                           |
    | `*`            | All files in top-level directory.                                      |
    | `.*`           | File names beginning with '.'.                                         |
    | `*word*`       | All files with 'word' in the filename.                                 |
    | `readme.*`     | All files named 'readme' with any file extension.                      |
    | `styles/*.css` | All files with extension '.css' in the directory 'styles/'.            |
    | `scripts/*/*`  | All files in 'scripts/' or one level of subdirectory under 'scripts/'. |
    | `images*/*`    | All files in a folder with name that is or begins with 'images'.       |

- Arbitrary directory depth (`/**/`).

    | Value      | Description                                 |
    |------------|---------------------------------------------|
    | `**/*`     | All files in any subdirectory.              |
    | `dir/**/*` | All files in any subdirectory under 'dir/'. |

- Relative paths.

    To match all files in a directory named "shared" at the sibling level to the base directory, use `../shared/*`.