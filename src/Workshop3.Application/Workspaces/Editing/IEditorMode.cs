using Workshop3.Application.Models;
using Workshop3.Application.Workspaces.Editing.KeyHandling;

namespace Workshop3.Application.Workspaces.Editing;

public interface IEditorMode : IKeyHandler
{
    EditorMode Mode { get; }

    DocumentSelection MoveTo(DocumentSelection selection, DocumentPosition target);
}
