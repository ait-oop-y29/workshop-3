using System.Collections;
using Workshop3.Application.Models;

namespace Workshop3.Application.FileSystem.Enumeration;

public sealed class FlatFileEnumerable(IFileSystemNode root) : IEnumerable<FlattenedFile>
{
    public IEnumerator<FlattenedFile> GetEnumerator() => new FlatFileEnumerator(root);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
