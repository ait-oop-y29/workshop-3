using Workshop3.Application.FileSystem;
using Workshop3.Application.VersionControl.Models;

namespace Workshop3.Application.VersionControl;

public interface IVersionControlSystem : IDisposable
{
    IEnumerable<Commit> EnumerateCommits();

    IFileSystemNode GetHeadNode();

    IFileSystemNode GetNodeByHash(VersionControlHash? hash);

    VersionControlHash CreateCommit(IFileSystemNode fileSystem, string message);

    void RollbackTo(VersionControlHash hash);
}
