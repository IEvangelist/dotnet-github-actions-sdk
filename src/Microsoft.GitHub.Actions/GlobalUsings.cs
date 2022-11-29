// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

global using System.Runtime.CompilerServices;
global using System.Text;
global using System.Text.Encodings.Web;
global using System.Text.Json;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.GitHub.Actions.Commands;
global using Microsoft.GitHub.Actions.Extensions;
global using Microsoft.GitHub.Actions.Output;
global using Microsoft.GitHub.Actions.Services;
global using Microsoft.GitHub.Actions.Workflows;

global using static System.Environment;
global using static System.IO.Path;

global using static Microsoft.GitHub.EnvironmentVariables.Keys;
global using static Microsoft.GitHub.EnvironmentVariables.Prefixes;
global using static Microsoft.GitHub.EnvironmentVariables.Suffixes;
