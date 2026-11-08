using Workshop3.Application.FileSystem.Content;
using Workshop3.Application.Models;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class InsertCharacterCommand(Key key) : IWorkspaceCommand
{
    public WorkspaceCommandResult Execute(Workspace workspace)
    {
        if (workspace.SelectedFile is null
            || workspace.SelectedFile.TryGetEditableContent(out IEditableFileContent? content) is false)
        {
            return new WorkspaceCommandResult.Failure("No file selected");
        }

        DocumentPosition position = workspace.Selection.End;

        IEditableLineContent line = content.GetEditableLine(position.Line);
        line.InsertAt(position.Character, key.Value);

        position = position with { Character = position.Character + 1 };
        workspace.MoveTo(position);

        return new WorkspaceCommandResult.Success();
    }
}
