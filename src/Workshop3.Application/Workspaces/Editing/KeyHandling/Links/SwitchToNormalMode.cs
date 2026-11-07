using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling.Links;

public sealed class SwitchToNormalMode : KeyHandlerBase
{
    private readonly string _key;

    private SwitchToNormalMode(string key)
    {
        _key = key;
    }

    public static SwitchToNormalMode Escape => new("Escape");
    public static SwitchToNormalMode Custom(string key) => new(key);

    public override IWorkspaceCommand HandleKey(Key key)
    {
        return key.Is(_key) ? new SwitchModeCommand(new NormalEditorMode()) : CallNext(key);
    }
}
