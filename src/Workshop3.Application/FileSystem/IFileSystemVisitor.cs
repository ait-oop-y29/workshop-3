namespace Workshop3.Application.FileSystem;

public interface IFileSystemVisitor
{
    void Visit(IFileNode node);

    void Visit(IDirectoryNode node);
}
