namespace Workshop3.Application.FileSystem;

public interface IDirectoryNode : IFileSystemNode, IEnumerable<IFileSystemNode>;
