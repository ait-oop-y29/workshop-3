using System.IO.Compression;
using Workshop3.Application.FileSystem;
using Workshop3.Application.VersionControl.CommitStorage;
using Workshop3.Application.VersionControl.Metadata;
using Workshop3.Application.VersionControl.Models;
using Workshop3.Application.VersionControl.ObjectStorage;

namespace Workshop3.Application.VersionControl;

public sealed class ZipVersionControlSystem(
    ZipArchive archive)
    : IVersionControlSystem
{
    private readonly VersionControlSystem _versionControlSystem = new(
        new ZipObjectStorage(archive, "objects"),
        new ZipCommitStorage(archive, "commits"),
        new ZipMetadataStorage(archive, "metadata.json"));

    public IFileSystemNode GetHeadNode()
        => _versionControlSystem.GetHeadNode();

    public IEnumerable<Commit> EnumerateCommits()
        => _versionControlSystem.EnumerateCommits();

    public IFileSystemNode GetNodeByHash(VersionControlHash? hash)
        => _versionControlSystem.GetNodeByHash(hash);

    public VersionControlHash CreateCommit(IFileSystemNode fileSystem, string message)
        => _versionControlSystem.CreateCommit(fileSystem, message);

    public void RollbackTo(VersionControlHash hash)
        => _versionControlSystem.RollbackTo(hash);

    public void Dispose()
    {
        _versionControlSystem.Dispose();
        archive.Dispose();
    }
}
