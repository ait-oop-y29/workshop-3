using System.Collections;

namespace Workshop3.Application.FileSystem.Editable;

public sealed class EditableDirectoryNode : IDirectoryNode
{
    private readonly SortedList<string, IFileSystemNode> _children;

    public EditableDirectoryNode(
        string name,
        IEnumerable<IFileSystemNode>? children = null)
    {
        Name = name;
        _children = new SortedList<string, IFileSystemNode>(StringComparer.Ordinal);

        foreach (IFileSystemNode child in children ?? [])
        {
            _children.Add(child.Name, child);
        }
    }

    public string Name { get; }

    public IEnumerator<IFileSystemNode> GetEnumerator() => _children.Select(kvp => kvp.Value).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(IFileSystemNode child) => _children.Add(child.Name, child);

    public void Accept(IFileSystemVisitor visitor)
    {
        visitor.Visit(this);
    }
}
