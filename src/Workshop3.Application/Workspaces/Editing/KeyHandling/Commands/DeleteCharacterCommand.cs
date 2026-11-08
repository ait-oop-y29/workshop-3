using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class DeleteCharacterCommand : IWorkspaceCommand
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

        if (position.Character > 0)
        {
            line.RemoveAt(position.Character - 1);

            position = position with { Character = position.Character - 1 };
            workspace.MoveTo(position);
        }
        else if (position.Line > 0)
        {
            IEditableLineContent previousLine = content.GetEditableLine(position.Line - 1);
            int length = previousLine.Length;

            previousLine.InsertAt(length, line.ToString());
            content.RemoveLine(position.Line);

            position = new DocumentPosition(Line: position.Line - 1, Character: length);
            workspace.MoveTo(position);
        }

        return new WorkspaceCommandResult.Success();
    }
}
