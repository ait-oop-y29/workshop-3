using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class MoveUpLink : KeyHandlerBase
{
    private readonly string _key;

    private MoveUpLink(string key)
    {
        _key = key;
    }

    public static MoveUpLink Arrow => new("ArrowUp");
    public static MoveUpLink Letter => new("k");

    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is(_key) ? new MoveUpCommand() : CallNext(key);
    }
}
