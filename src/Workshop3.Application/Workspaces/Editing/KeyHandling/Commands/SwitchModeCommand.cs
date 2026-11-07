namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class SwitchModeCommand(IEditorMode mode) : IWorkspaceCommand
{
    public WorkspaceCommandResult Execute(Workspace workspace)
    {
        workspace.SwitchMode(mode);
        workspace.MoveTo(workspace.Selection.End);
        
        return new WorkspaceCommandResult.Success();
    }
}
