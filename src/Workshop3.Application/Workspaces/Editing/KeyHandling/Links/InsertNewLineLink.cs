using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class InsertNewLineLink : KeyHandlerBase
{
    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is("Enter") || key.Is("Return") ? new InsertNewLineCommand() : CallNext(key);
    }
}
