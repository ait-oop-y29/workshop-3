using Workshop3.Application.FileSystem;
using Workshop3.Application.VersionControl.Models;
using Workshop3.Application.VersionControl.ObjectStorage;

namespace Workshop3.Application.VersionControl.Files;

public sealed class VersionControlDirectoryBuilder(
    string name,
    IVersionControlObjectStorage objectStorage)
{
    private readonly List<VersionControlFileNode> _files = [];
    private readonly Dictionary<string, VersionControlDirectoryBuilder> _directories = [];

    public void AddFile(CommitFile commitFile)
    {
        string[] parts = Path.GetDirectoryName(commitFile.Path)?.Split(Path.DirectorySeparatorChar) ?? [];
        using IEnumerator<string> partsEnumerator = parts.AsEnumerable().GetEnumerator();

        var file = new VersionControlFileNode(
            Path.GetFileName(commitFile.Path),
            commitFile.ObjectHash,
            objectStorage);

        AddFile(partsEnumerator, file);
    }

    public IFileSystemNode Build()
    {
        IEnumerable<IFileSystemNode> directories = _directories.Values.Select(builder => builder.Build());
        return new VersionControlDirectoryNode(name, nodes: directories.Concat(_files));
    }

    private void AddFile(
        IEnumerator<string> pathPartEnumerator,
        VersionControlFileNode file)
    {
        if (pathPartEnumerator.MoveNext() is false)
        {
            _files.Add(file);
        }
        else
        {
            if (_directories.TryGetValue(pathPartEnumerator.Current, out var directory) is false)
            {
                directory = new VersionControlDirectoryBuilder(pathPartEnumerator.Current, objectStorage);
                _directories[pathPartEnumerator.Current] = directory;
            }

            directory.AddFile(pathPartEnumerator, file);
        }
    }
}
