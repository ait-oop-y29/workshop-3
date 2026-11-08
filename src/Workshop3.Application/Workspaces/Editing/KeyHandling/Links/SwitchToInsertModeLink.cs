using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class SwitchToInsertModeLink : KeyHandlerBase
{
    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is("i") ? new SwitchModeCommand(new InsertEditorMode()) : CallNext(key);
    }
}
