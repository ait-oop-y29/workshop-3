using Workshop3.Application.VersionControl.Models;

namespace Workshop3.Application.VersionControl.Metadata;

public interface IVersionControlMetadataStorage
{
    VersionControlHash? GetHeadCommitHash();

    void UpdateHeadCommitHash(VersionControlHash hash);
}
