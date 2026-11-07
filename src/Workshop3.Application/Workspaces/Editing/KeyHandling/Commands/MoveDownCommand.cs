using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class MoveDownCommand : IWorkspaceCommand
{
    public WorkspaceCommandResult Execute(Workspace workspace)
    {
        if (workspace.SelectedFile is null)
            return new WorkspaceCommandResult.Failure("No file selected");

        DocumentPosition end = workspace.Selection.End;

        IFileContent content = workspace.SelectedFile.GetContent();

        if (end.Line < content.LineCount - 1)
        {
            ILineContent next = content.GetLine(end.Line + 1);

            end = new DocumentPosition(
                Line: end.Line + 1,
                Character: Math.Min(end.Character, next.Length));
        }
        else
        {
            ILineContent line = content.GetLine(end.Line);
            end = end with { Character = line.Length };
        }

        workspace.MoveTo(end);

        return new WorkspaceCommandResult.Success();
    }
}
