using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

namespace Workshop3.Application.Workspaces.Editing;

public sealed class VisualEditorMode : IEditorMode
{
    private readonly IKeyHandler _keyHandler =
        MoveRightLink.Arrow
            .AddNext(MoveRightLink.Letter)
            .AddNext(MoveLeftLink.Arrow)
            .AddNext(MoveLeftLink.Letter)
            .AddNext(MoveUpLink.Arrow)
            .AddNext(MoveUpLink.Letter)
            .AddNext(MoveDownLink.Arrow)
            .AddNext(MoveDownLink.Letter)
            .AddNext(SwitchToNormalMode.Escape)
            .AddNext(SwitchToNormalMode.Custom("v"));

    public EditorMode Mode => EditorMode.Visual;

    public DocumentSelection MoveTo(DocumentSelection selection, DocumentPosition target)
        => selection.WithEnd(target);

    public IWorkspaceCommand HandleKey(Key key)
        => _keyHandler.HandleKey(key);
}
