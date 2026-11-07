using System.Diagnostics;
using Workshop3.Application.FileSystem;
using Workshop3.Application.FileSystem.Visitors;
using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing;
using Workshop3.Application.Workspaces.Editing.KeyHandling;

namespace Workshop3.Application.Workspaces;

public sealed class Workspace(IFileSystemNode fileSystem)
{
    private IEditorMode _mode = new NormalEditorMode();

    public IFileSystemNode FileSystem { get; private set; } = fileSystem;

    public IFileNode? SelectedFile { get; private set; }

    public EditorMode Mode => _mode.Mode;

    public DocumentSelection Selection { get; private set; } = DocumentSelection.Default;

    public void ChangeSelectedFile(IFileNode? file)
    {
        SelectedFile = file;
        _mode = new InsertEditorMode();
        Selection = DocumentSelection.Default;
    }

    public void MoveTo(DocumentPosition end)
    {
        Selection = _mode.MoveTo(Selection, end);
    }

    public void SwitchMode(IEditorMode mode)
    {
        _mode = mode;
    }

    public HandleKeyResult OnKeyPressed(Key key)
    {
        IWorkspaceCommand command = _mode.HandleKey(key);
        WorkspaceCommandResult result = command.Execute(this);

        return result switch
        {
            WorkspaceCommandResult.Success => new HandleKeyResult.Success(),
            WorkspaceCommandResult.Failure failure => new HandleKeyResult.Failure(failure.ErrorMessage),
            _ => throw new UnreachableException(),
        };
    }

    public IFileSystemNode CreateSnapshot()
    {
        var visitor = new FileSystemCopyVisitor();
        FileSystem.Accept(visitor);

        return visitor.Clone;
    }

    public void RestoreSnapshot(IFileSystemNode node)
    {
        var visitor = new FileSystemCopyVisitor();
        node.Accept(visitor);

        FileSystem = visitor.Clone;
    }
}
