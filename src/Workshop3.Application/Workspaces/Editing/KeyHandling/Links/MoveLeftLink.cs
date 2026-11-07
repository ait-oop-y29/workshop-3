using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class MoveLeftLink : KeyHandlerBase
{
    private readonly string _key;

    private MoveLeftLink(string key)
    {
        _key = key;
    }

    public static MoveLeftLink Arrow => new("ArrowLeft");
    public static MoveLeftLink Letter => new("h");

    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is(_key) ? new MoveLeftCommand() : CallNext(key);
    }
}
