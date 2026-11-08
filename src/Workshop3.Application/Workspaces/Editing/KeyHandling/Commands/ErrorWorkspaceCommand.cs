namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class ErrorWorkspaceCommand(string errorMessage) : IWorkspaceCommand
{
    public WorkspaceCommandResult Execute(Workspace workspace)
    {
        return new WorkspaceCommandResult.Failure(errorMessage);
    }
}
