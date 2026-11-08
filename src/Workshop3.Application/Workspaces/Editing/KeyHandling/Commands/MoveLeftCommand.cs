using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class MoveLeftCommand : IWorkspaceCommand
{
    public WorkspaceCommandResult Execute(Workspace workspace)
    {
        if (workspace.SelectedFile is null)
            return new WorkspaceCommandResult.Failure("No file selected");

        DocumentPosition end = workspace.Selection.End;

        IFileContent content = workspace.SelectedFile.GetContent();

        if (end.Character > 0)
        {
            end = end with { Character = end.Character - 1 };
        }
        else if (end.Line > 0)
        {
            ILineContent previous = content.GetLine(end.Line - 1);
            end = new DocumentPosition(Line: end.Line - 1, Character: previous.Length - 1);
        }

        workspace.MoveTo(end);

        return new WorkspaceCommandResult.Success();
    }
}
