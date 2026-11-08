using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class MoveUpCommand : IWorkspaceCommand
{
    public WorkspaceCommandResult Execute(Workspace workspace)
    {
        if (workspace.SelectedFile is null)
            return new WorkspaceCommandResult.Failure("No file selected");

        DocumentPosition end = workspace.Selection.End;

        IFileContent content = workspace.SelectedFile.GetContent();

        if (end.Line > 0)
        {
            ILineContent previous = content.GetLine(end.Line - 1);

            end = new DocumentPosition(
                Line: end.Line - 1,
                Character: Math.Min(end.Character, previous.Length));
        }
        else
        {
            end = end with { Character = 0 };
        }

        workspace.MoveTo(end);

        return new WorkspaceCommandResult.Success();
    }
}
