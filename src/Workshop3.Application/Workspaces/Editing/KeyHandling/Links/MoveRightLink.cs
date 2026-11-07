using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class MoveRightLink : KeyHandlerBase
{
    private readonly string _key;

    private MoveRightLink(string key)
    {
        _key = key;
    }

    public static MoveRightLink Arrow => new("ArrowRight");
    public static MoveRightLink Letter => new("l");

    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is(_key) ? new MoveRightCommand() : CallNext(key);
    }
}
