using Workshop3.Application.Models;

namespace Workshop3.Application.FileSystem.Enumeration;

public static class FineSystemNodeExtensions
{
    public static IEnumerable<FlattenedFile> Flatten(this IFileSystemNode node)
        => new FlatFileEnumerable(node);
}
