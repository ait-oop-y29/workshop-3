using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class MoveDownLink : KeyHandlerBase
{
    private readonly string _key;

    private MoveDownLink(string key)
    {
        _key = key;
    }

    public static MoveDownLink Arrow => new("ArrowDown");
    public static MoveDownLink Letter => new("j");

    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is(_key) ? new MoveDownCommand() : CallNext(key);
    }
}
