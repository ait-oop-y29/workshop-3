using System.Diagnostics.CodeAnalysis;
using Workshop3.Application.FileSystem;
using Workshop3.Application.FileSystem.Content;
using Workshop3.Application.VersionControl.Models;
using Workshop3.Application.VersionControl.ObjectStorage;

namespace Workshop3.Application.VersionControl.Files;

public sealed class VersionControlFileNode(
    string name,
    VersionControlHash hash,
    IVersionControlObjectStorage storage)
    : IFileNode
{
    private readonly VersionControlFileContent _content = new(hash, storage);

    public string Name => name;

    public IFileContent GetContent() => _content;

    public bool TryGetEditableContent(
        [NotNullWhen(true)] out IEditableFileContent? content)
    {
        content = null;
        return false;
    }

    public void Accept(IFileSystemVisitor visitor)
    {
        visitor.Visit(this);
    }
}
