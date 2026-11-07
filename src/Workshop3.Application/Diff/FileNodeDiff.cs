using Workshop3.Application.FileSystem;

namespace Workshop3.Application.Diff;

public abstract record FileNodeDiff
{
    private FileNodeDiff() { }

    public sealed record Added(string Path, IFileNode File) : FileNodeDiff;

    public sealed record Changed(string Path, IFileNode OldFile, IFileNode NewFile) : FileNodeDiff;

    public sealed record Removed(string Path, IFileNode File) : FileNodeDiff;
}
