// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

global using System.Diagnostics;
global using System.Runtime.CompilerServices;
global using System.Text;
global using System.Text.Encodings.Web;
global using System.Text.Json;

global using Actions.Core.Commands;
global using Actions.Core.Extensions;

global using Microsoft.Extensions.DependencyInjection;
global using Actions.Core.Output;
global using Actions.Core.Services;
global using Actions.Core.Workflows;

global using static System.Environment;
global using static System.IO.Path;
global using static Actions.Octokit.EnvironmentVariables.Keys;
global using static Actions.Octokit.EnvironmentVariables.Prefixes;
global using static Actions.Octokit.EnvironmentVariables.Suffixes;
