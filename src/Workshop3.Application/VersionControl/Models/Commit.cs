namespace Workshop3.Application.VersionControl.Models;

public sealed record Commit(
    VersionControlHash Hash,
    VersionControlHash? ParentHash,
    string Message,
    IReadOnlyCollection<CommitFile> Files);
