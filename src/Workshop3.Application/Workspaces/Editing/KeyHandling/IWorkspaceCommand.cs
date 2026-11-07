namespace Workshop3.Application.Workspaces.Editing.KeyHandling;

public interface IWorkspaceCommand
{
    WorkspaceCommandResult Execute(Workspace workspace);
}
