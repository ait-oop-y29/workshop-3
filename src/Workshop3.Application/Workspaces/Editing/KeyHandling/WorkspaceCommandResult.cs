namespace Workshop3.Application.Workspaces.Editing.KeyHandling;

public abstract record WorkspaceCommandResult
{
    private WorkspaceCommandResult() { }

    public sealed record Success : WorkspaceCommandResult;

    public sealed record Failure(string ErrorMessage) : WorkspaceCommandResult;
}
