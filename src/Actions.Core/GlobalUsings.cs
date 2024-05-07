// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

global using System.Diagnostics;
global using System.Globalization;
global using System.Runtime.CompilerServices;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization.Metadata;

global using Actions.Core.Commands;
global using Actions.Core.Extensions;
global using Actions.Core.Markdown;
global using Actions.Core.Output;
global using Actions.Core.Services;
global using Actions.Core.Summaries;
global using Actions.Core.Workflows;

global using Microsoft.Extensions.DependencyInjection;

global using static System.Environment;
global using static System.IO.Path;

global using static Actions.Core.EnvironmentVariables.Keys;
global using static Actions.Core.EnvironmentVariables.Prefixes;
global using static Actions.Core.EnvironmentVariables.Suffixes;
