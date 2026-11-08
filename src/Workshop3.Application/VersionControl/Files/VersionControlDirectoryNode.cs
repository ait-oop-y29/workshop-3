using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Workshop3.Application.FileSystem;

namespace Workshop3.Application.VersionControl.Files;

public sealed class VersionControlDirectoryNode : IDirectoryNode
{
    private readonly SortedList<string, IFileSystemNode> _children;

    public VersionControlDirectoryNode(
        string name,
        IEnumerable<IFileSystemNode> nodes)
    {
        Name = name;
        _children = new SortedList<string, IFileSystemNode>(StringComparer.Ordinal);

        foreach (IFileSystemNode node in nodes)
        {
            _children.Add(node.Name, node);
        }
    }

    public string Name { get; }

    public IEnumerator<IFileSystemNode> GetEnumerator() => _children.Select(kvp => kvp.Value).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    public void Accept(IFileSystemVisitor visitor)
    {
        visitor.Visit(this);
    }
}
