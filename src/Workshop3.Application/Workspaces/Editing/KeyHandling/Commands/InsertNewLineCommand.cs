using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

public sealed class InsertNewLineCommand : IWorkspaceCommand
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
        var lineContent = line.ToString();

        string remainer = lineContent.Length > position.Character
            ? lineContent[position.Character..]
            : string.Empty;

        if (remainer.Length > 0)
        {
            line.RemoveAt(position.Character, length: remainer.Length);
        }

        content.InsertLine(position.Line + 1, remainer);
        workspace.MoveTo(new DocumentPosition(position.Line + 1, 0));

        return new WorkspaceCommandResult.Success();
    }
}
