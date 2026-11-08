using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class DeleteCharacterLink : KeyHandlerBase
{
    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is("Backspace") ? new DeleteCharacterCommand() : CallNext(key);
    }
}
