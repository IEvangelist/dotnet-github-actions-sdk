# Toolkit Packages

The following table tracks the various packages development progress. Each package strives to provide functional equivalence with its corresponding `@actions/toolkit` package, but with the following additional features:

- **Testable**: The package is designed to be testable, with a clear separation between the core logic and the I/O operations. This allows for easier testing and mocking of the package's behavior.
- **DI friendly**: The package is designed to be dependency injection friendly, allowing for easier mocking and testing of the package's behavior.
- **README.md**: The package has a `README.md` file that describes its usage and behavior.
- **Tests**: The package has a test suite that validates its behavior.
- **Attribution**: The package has a clear attribution to the original `@actions/toolkit` package and any other 3rd party OSS packages that it depends on.

| `@actions/toolkit` | Package | Exists? | Testable? | DI Friendly? | README? | Tests? | Attribution? |
|--|--|:--:|:--:|:--:|:--:|:--:|:--:|
| `@actions/attest` | `GitHubActions.Toolkit.Attest` | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 |
| `@actions/cache` | `GitHubActions.Toolkit.Cache` | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 |
| `@actions/core` | `GitHubActions.Toolkit.Core` | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| `@actions/download-artifact` | `GitHubActions.Toolkit.Artifact` | ✅ | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 |
| `@actions/exec` | `GitHubActions.Toolkit.Exec` | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 |
| `@actions/github` | `GitHubActions.Toolkit.Octokit` | ✅ | ✅ | ✅ | ✅ | ✅ | 🔳 |
| `@actions/http-client` | `GitHubActions.Toolkit.HttpClient` | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 |
| `@actions/io` | `GitHubActions.Toolkit.IO` | ✅ | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 |
| `@actions/tool-cache` | `GitHubActions.Toolkit.ToolCache` | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 |
| `@actions/upload-artifact` | `GitHubActions.Toolkit.Artifact` | ✅ | 🔳 | 🔳 | 🔳 | 🔳 | 🔳 |

**Legend**

- **✅**: Done
- **🔳**: Not done

## Testable

Each package should strive to be testable, such that consumers can test all aspects of an API surface area with ease.

## DI Friendly

Going hand-in-hand with being testable, each package should strive to be dependency injection friendly, such that consumers register services via an `Add*` extension method on the `IServiceCollection` type.

## README.md

All packages require a `README.md` file that describes its usage and behavior. While they can be similar or derived from the original, it's best to keep these concise as they'll need to be packaged within the NuGet and link to a more thorough doc.

## Tests

All packages require a test suite that validates its behavior. This is a requirement for all packages, as it ensures that the package behaves as expected. Additionally, tests are a great way for consumers to learn how a bit of functionality is intended to behave.

## Attribution

Each package is built atop various other packages, and it's important to give credit where credit is due. This includes the original `@actions/toolkit` package, as well as any other 3rd party OSS packages that the package depends on.
