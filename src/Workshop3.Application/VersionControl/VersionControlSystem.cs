using System.Security.Cryptography;
using System.Text;
using Workshop3.Application.FileSystem;
using Workshop3.Application.FileSystem.Content;
using Workshop3.Application.FileSystem.Enumeration;
using Workshop3.Application.Models;
using Workshop3.Application.VersionControl.CommitStorage;
using Workshop3.Application.VersionControl.Files;
using Workshop3.Application.VersionControl.Metadata;
using Workshop3.Application.VersionControl.Models;
using Workshop3.Application.VersionControl.ObjectStorage;

namespace Workshop3.Application.VersionControl;

public sealed class VersionControlSystem(
    IVersionControlObjectStorage objectStorage,
    IVersionControlCommitStorage commitStorage,
    IVersionControlMetadataStorage metadataStorage)
    : IVersionControlSystem
{
    public IFileSystemNode GetHeadNode()
    {
        VersionControlHash? headHash = metadataStorage.GetHeadCommitHash();
        return GetNodeByHash(headHash);
    }

    public IEnumerable<Commit> EnumerateCommits()
    {
        var commits = commitStorage
            .EnumerateCommits()
            .ToDictionary(commit => commit.Hash);

        VersionControlHash? headCommitHash = metadataStorage.GetHeadCommitHash();

        if (headCommitHash is null)
            yield break;

        Commit current = commits[headCommitHash.Value];
        yield return current;

        while (current.ParentHash is not null)
        {
            current = commits[current.ParentHash.Value];
            yield return current;
        }
    }

    public IFileSystemNode GetNodeByHash(VersionControlHash? hash)
    {
        if (hash is null)
            return new VersionControlDirectoryNode(string.Empty, []);

        Commit commit = commitStorage
            .EnumerateCommits()
            .Single(commit => commit.Hash.Equals(hash.Value));

        var builder = new VersionControlDirectoryBuilder(string.Empty, objectStorage);

        foreach (CommitFile file in commit.Files)
        {
            builder.AddFile(file);
        }

        return builder.Build();
    }

    public VersionControlHash CreateCommit(IFileSystemNode fileSystem, string message)
    {
        Commit[] commits = commitStorage.EnumerateCommits().ToArray();

        var parentHashes = commits
            .Select(commit => commit.ParentHash)
            .ToHashSet();

        Commit? parentCommit = commits.SingleOrDefault(commit => parentHashes.Contains(commit.Hash) is false);

        using var hash = IncrementalHash.CreateHash(HashAlgorithmName.SHA1);
        var commitFiles = new List<CommitFile>();

        if (parentCommit is not null)
        {
            hash.AppendData(Encoding.Default.GetBytes(parentCommit.Hash.Value));
        }

        foreach (FlattenedFile flattenedFile in fileSystem.Flatten())
        {
            IFileContent content = flattenedFile.File.GetContent();

            hash.AppendData(Encoding.Default.GetBytes(flattenedFile.File.Name));
            hash.AppendData(Encoding.Default.GetBytes(content.ToString()));

            VersionControlHash fileHash = CreateObject(content.ToString());

            var commitFile = new CommitFile(Path: flattenedFile.FullPath, ObjectHash: fileHash);
            commitFiles.Add(commitFile);
        }

        var commitHash = new VersionControlHash(Convert.ToHexString(hash.GetCurrentHash()));

        var commit = new Commit(
            commitHash,
            parentCommit?.Hash,
            message,
            commitFiles);

        commitStorage.CreateCommit(commit);
        metadataStorage.UpdateHeadCommitHash(commitHash);

        return commitHash;
    }

    public void RollbackTo(VersionControlHash hash)
    {
        Commit[] commits = EnumerateCommits()
            .TakeWhile(commit => commit.Hash.Equals(hash) is false)
            .ToArray();

        foreach (Commit commit in commits)
        {
            commitStorage.DropCommit(commit.Hash);
        }

        metadataStorage.UpdateHeadCommitHash(hash);
    }

    private VersionControlHash CreateObject(string content)
    {
        byte[] contentBytes = Encoding.Default.GetBytes(content);
        byte[] hashBytes = SHA1.HashData(contentBytes);

        var hash = new VersionControlHash(Convert.ToHexString(hashBytes));

        objectStorage.CreateObject(hash, content);

        return hash;
    }

    public void Dispose() { }
}
