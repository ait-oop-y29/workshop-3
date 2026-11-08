using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

namespace Workshop3.Application.Workspaces.Editing;

public sealed class InsertEditorMode : IEditorMode
{
    private readonly IKeyHandler _keyHandler =
        MoveRightLink.Arrow
            .AddNext(MoveLeftLink.Arrow)
            .AddNext(MoveDownLink.Arrow)
            .AddNext(MoveUpLink.Arrow)
            .AddNext(SwitchToNormalMode.Escape)
            .AddNext(new InsertCharacterLink())
            .AddNext(new InsertNewLineLink())
            .AddNext(new DeleteCharacterLink());

    public EditorMode Mode => EditorMode.Insert;

    public DocumentSelection MoveTo(DocumentSelection selection, DocumentPosition target)
        => selection.WithStart(target).WithEnd(target);

    public IWorkspaceCommand HandleKey(Key key)
        => _keyHandler.HandleKey(key);
}
