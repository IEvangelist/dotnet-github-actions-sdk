// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.GitHub.Actions.Services;

public interface IWorkflowStepService
{
    void ExportVariable(string name, string value);

    void SetSecret(string secret);

    ValueTask AddPathAsync(string path);

    string GetInput(string name, InputOptions? options = null);

    string[] GetMultilineInput(string name, InputOptions? options = null);

    bool GetBoolInput(string name, InputOptions? options = null);

    Task SetOutputAsync(string name, string value);

    void SetCommandEcho(bool enabled);

    void SetFailed(string message);

    bool IsDebug { get; }

    void Debug(string message);

    void Error(string message, AnnotationProperties? properties = null);

    void Warning(string message, AnnotationProperties? properties = null);

    void Notice(string message, AnnotationProperties? properties = null);

    void Info(string message);

    void StartGroup(string name);

    void EndGroup();

    Task<T> GroupAsync<T>(string name, Func<Task<T>> action);

    Task SaveStateAsync(string name, string value);

    string GetState(string name);
}
