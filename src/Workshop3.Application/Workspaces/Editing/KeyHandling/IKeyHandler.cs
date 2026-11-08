using Workshop3.Application.Models;

namespace Workshop3.Application.Workspaces.Editing.KeyHandling;

public interface IKeyHandler
{
    IWorkspaceCommand HandleKey(Key key);
}
