namespace Workshop3.Application.VersionControl.Models;

public sealed record CommitFile(
    string Path,
    VersionControlHash ObjectHash);
