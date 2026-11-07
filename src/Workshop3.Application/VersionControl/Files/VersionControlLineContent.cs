using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.VersionControl.Files;

public sealed class VersionControlLineContent(
    string line)
    : ILineContent
{
    public int Length => line.Length;

    public override string ToString() => line;
}
