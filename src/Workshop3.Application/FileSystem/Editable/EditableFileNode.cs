using System.Diagnostics.CodeAnalysis;
using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.FileSystem.Editable;

public sealed class EditableFileNode(
    string name,
    string? content = null)
    : IFileNode
{
    private readonly EditableFileContent _content = new(content?.Split("\n") ?? []);

    public string Name { get; } = name;

    public IFileContent GetContent()
    {
        return _content;
    }

    public bool TryGetEditableContent([NotNullWhen(true)] out IEditableFileContent? content)
    {
        content = _content;
        return true;
    }
    
    public void Accept(IFileSystemVisitor visitor)
    {
        visitor.Visit(this);
    }
}
