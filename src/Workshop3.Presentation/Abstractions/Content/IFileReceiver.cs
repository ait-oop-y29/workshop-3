using Workshop3.Application.FileSystem;

namespace Workshop3.Presentation.Abstractions.Content;

public interface IFileReceiver
{
    void OnFileChanged(IFileNode? file);
}
