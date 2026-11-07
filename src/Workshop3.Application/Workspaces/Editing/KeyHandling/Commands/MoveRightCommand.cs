using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class MoveRightCommand : IWorkspaceCommand
{
    public WorkspaceCommandResult Execute(Workspace workspace)
    {
        if (workspace.SelectedFile is null)
            return new WorkspaceCommandResult.Failure("No file selected");

        DocumentPosition end = workspace.Selection.End;

        IFileContent content = workspace.SelectedFile.GetContent();
        ILineContent line = content.GetLine(end.Line);

        if (line.Length >= end.Character + 1)
        {
            end = end with { Character = end.Character + 1 };
        }
        else if (content.LineCount > end.Line)
        {
            end = new DocumentPosition(Line: end.Line + 1, Character: 0);
        }

        workspace.MoveTo(end);

        return new WorkspaceCommandResult.Success();
    }
}
