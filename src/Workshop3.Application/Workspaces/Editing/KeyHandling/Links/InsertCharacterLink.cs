using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class InsertCharacterLink : KeyHandlerBase
{
    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Length is 1 ? new InsertCharacterCommand(key) : CallNext(key);
    }
}
