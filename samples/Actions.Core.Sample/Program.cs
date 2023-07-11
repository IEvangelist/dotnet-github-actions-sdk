// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

using var services = new ServiceCollection()
    .AddGitHubActionsCore()
    .BuildServiceProvider();

var core = services.GetRequiredService<ICoreService>();

try
{
    // "who-to-greet" input defined in action metadata file
    var nameToGreet = core.GetInput("who-to-greet");
    core.Info($"Hello {nameToGreet}!");
    await core.SetOutputAsync("time", DateTime.UtcNow.ToString("o"));

    // Get the JSON webhook payload for the event that triggered the workflow
    var payload = JsonSerializer.Serialize(Context.Current.Payload);
    core.Info($"The event payload: {payload}");

    await core.SetOutputAsync("yesItWorks", new[]
    {
        "testing/this/out"
    });
}
catch (Exception ex)
{
    core.SetFailed(ex.ToString());
}