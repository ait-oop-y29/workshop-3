using Workshop3.Application.VersionControl.Models;

namespace Workshop3.Application.VersionControl.CommitStorage;

public interface IVersionControlCommitStorage
{
    IEnumerable<Commit> EnumerateCommits();

    void CreateCommit(Commit commit);

    void DropCommit(VersionControlHash hash);
}
