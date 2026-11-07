using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

namespace Workshop3.Application.Workspaces.Editing;

public sealed class NormalEditorMode : IEditorMode
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
            .AddNext(new SwitchToInsertModeLink())
            .AddNext(new SwitchToVisualModeLink());
    
    public EditorMode Mode => EditorMode.Normal;

    public DocumentSelection MoveTo(DocumentSelection selection, DocumentPosition target) 
        => selection.WithStart(target).WithEnd(target);

    public IWorkspaceCommand HandleKey(Key key) 
        => _keyHandler.HandleKey(key);
}
