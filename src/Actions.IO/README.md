# GitHub `Actions.IO` .NET SDK

The .NET equivalent of the official GitHub [actions/toolkit](https://github.com/actions/toolkit) `@actions/io` project.

## Blog

[🔗 Hello from the GitHub `Actions.IO` .NET SDK](https://davidpine.net/blog/github-actions-sdk)

## Usage

### Installing the NuGet package 📦

Welcome to the `Actions.IO` .NET SDK. This SDK is used to create GitHub Actions in .NET. The SDK is a thin wrapper around the .NET implementation of the GitHub Actions a select few packages from the [`@actions/toolkit`](https://github.com/actions/toolkit).

> ![Warning]
> This package is **not** an official _Microsoft_ or _GitHub_ product. It is a community-driven project.

You"ll need to install the [GitHub `Actions.IO` .NET SDK](https://www.nuget.org/packages/Actions.IO) NuGet package to use the .NET APIs. The package is available on NuGet.org. The following is the command to install the package:

#### Adding package references

Either add the package reference to your project file:

```xml
<PackageReference Include="GitHub.Actions.Glob" />
```

Or use the [`dotnet add package`](https://learn.microsoft.com/dotnet/core/tools/dotnet-add-package) .NET CLI command:

```bash
dotnet add package Actions.IO
```


## `Actions.IO`

This was modified, but borrowed from the [_glob/README.md_](https://github.com/actions/toolkit/blob/main/packages/glob/README.md).

> You can use this package to search for files matching glob patterns.

### Basic usage
