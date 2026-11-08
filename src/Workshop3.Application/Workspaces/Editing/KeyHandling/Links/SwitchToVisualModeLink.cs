using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class SwitchToVisualModeLink : KeyHandlerBase
{
    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is("v") ? new SwitchModeCommand(new VisualEditorMode()) : CallNext(key);
    }
}
