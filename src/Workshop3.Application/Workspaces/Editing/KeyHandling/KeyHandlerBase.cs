using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling.Commands;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling;

public abstract class KeyHandlerBase : IKeyHandler
{
    private KeyHandlerBase? _next;

    public abstract IWorkspaceCommand HandleKey(Key key);

    public KeyHandlerBase AddNext(KeyHandlerBase next)
    {
        if (_next is null)
        {
            _next = next;
        }
        else
        {
            _next.AddNext(next);
        }

        return this;
    }

    protected IWorkspaceCommand CallNext(Key key)
    {
        return _next?.HandleKey(key) ?? new ErrorWorkspaceCommand($"Invalid key = {key}");
    }
}
