namespace Workshop3.Application.FileSystem;

public interface IFileSystemNode
{
    string Name { get; }

    void Accept(IFileSystemVisitor visitor);
}
