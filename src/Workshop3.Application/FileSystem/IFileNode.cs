using System.Diagnostics.CodeAnalysis;
using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.FileSystem;

public interface IFileNode : IFileSystemNode
{
    IFileContent GetContent();

    bool TryGetEditableContent([NotNullWhen(true)] out IEditableFileContent? content);
}
