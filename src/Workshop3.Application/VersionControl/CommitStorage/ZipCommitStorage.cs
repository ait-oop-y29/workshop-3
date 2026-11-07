using System.IO.Compression;
using System.Text.Json;
using Workshop3.Application.VersionControl.Models;

namespace Workshop3.Application.VersionControl.CommitStorage;

public sealed class ZipCommitStorage(
    ZipArchive archive,
    string path)
    : IVersionControlCommitStorage
{
    public IEnumerable<Commit> EnumerateCommits()
    {
        return archive.Entries
            .Where(entry => entry.FullName.StartsWith(path))
            .Select(entry =>
            {
                using Stream stream = entry.Open();
                Commit? commit = JsonSerializer.Deserialize<Commit>(stream);

                return commit ?? throw new InvalidOperationException("Failed to deserialize commit");
            });
    }

    public void CreateCommit(Commit commit)
    {
        string commitPath = Path.Combine(path, commit.Hash.Value);
        ZipArchiveEntry entry = archive.CreateEntry(commitPath);

        using Stream stream = entry.Open();
        JsonSerializer.Serialize(stream, commit);
    }

    public void DropCommit(VersionControlHash hash)
    {
        string commitPath = Path.Combine(path, hash.Value);
        archive.GetEntry(commitPath)?.Delete();
    }
}
