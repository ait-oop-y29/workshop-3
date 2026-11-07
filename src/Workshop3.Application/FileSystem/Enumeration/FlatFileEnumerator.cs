using System.Collections;
using Workshop3.Application.Models;

namespace Workshop3.Application.FileSystem.Enumeration;

public sealed class FlatFileEnumerator : IFileSystemVisitor, IEnumerator<FlattenedFile>
{
    private readonly Queue<FlattenedFile> _returnQueue = [];
    private readonly Queue<IDirectoryNode> _iterateQueue = [];
    private readonly Queue<string> _iteratePathQueue = [];

    private FlattenedFile? _current;

    FlattenedFile IEnumerator<FlattenedFile>.Current => _current ?? throw new InvalidOperationException("No current");
    object? IEnumerator.Current => _current;

    public FlatFileEnumerator(IFileSystemNode root)
    {
        root.Accept(this);
    }

    public bool MoveNext()
    {
        FlattenedFile? result;

        while (_returnQueue.TryDequeue(out result) is false
               && _iterateQueue.TryDequeue(out IDirectoryNode? directory))
        {
            foreach (IFileSystemNode child in directory)
            {
                child.Accept(this);
            }

            _iteratePathQueue.Dequeue();
        }

        _current = result;
        return result is not null;
    }

    public void Visit(IFileNode node)
    {
        string path = _iteratePathQueue.TryPeek(out string? result) ? Path.Combine(result, node.Name) : node.Name;
        _returnQueue.Enqueue(new FlattenedFile(path, node));
    }

    public void Visit(IDirectoryNode node)
    {
        _iterateQueue.Enqueue(node);

        string path = _iteratePathQueue.TryPeek(out string? result) ? Path.Combine(result, node.Name) : node.Name;
        _iteratePathQueue.Enqueue(path);
    }

    public void Reset()
    {
        throw new NotSupportedException();
    }

    public void Dispose() { }
}
