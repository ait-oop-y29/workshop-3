using Workshop3.Application.FileSystem.Editable;

namespace Workshop3.Application.FileSystem.Visitors;

public sealed class FileSystemCopyVisitor : IFileSystemVisitor
{
    private readonly Stack<EditableDirectoryNode> _nodes = [];

    public IFileSystemNode Clone => _nodes.Pop();

    public void Visit(IFileNode node)
    {
        if (_nodes.TryPeek(out EditableDirectoryNode? result))
        {
            result.Add(new EditableFileNode(node.Name, node.GetContent().ToString()));
        }
    }

    public void Visit(IDirectoryNode node)
    {
        var clone = new EditableDirectoryNode(node.Name);

        bool hasParent = _nodes.TryPeek(out EditableDirectoryNode? parent);

        if (hasParent && parent is not null)
        {
            parent.Add(clone);
        }

        _nodes.Push(clone);

        foreach (IFileSystemNode child in node)
        {
            child.Accept(this);
        }

        if (hasParent)
        {
            _nodes.Pop();
        }
    }
}
