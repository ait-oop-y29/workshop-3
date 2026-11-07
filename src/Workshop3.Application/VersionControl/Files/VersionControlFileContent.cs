using System.Collections;
using Workshop3.Application.FileSystem.Content;
using Workshop3.Application.VersionControl.Models;
using Workshop3.Application.VersionControl.ObjectStorage;

namespace Workshop3.Application.VersionControl.Files;

public sealed class VersionControlFileContent : IFileContent
{
    private readonly Lazy<string> _content;
    private readonly Lazy<IReadOnlyList<ILineContent>> _lines;

    public VersionControlFileContent(
        VersionControlHash hash,
        IVersionControlObjectStorage storage)
    {
        _content = new Lazy<string>(() => storage.TryReadObject(hash, out string? content) ? content : string.Empty);

        _lines = new Lazy<IReadOnlyList<ILineContent>>(() =>
        {
            return _content.Value
                .Split("\n")
                .Select(line => new VersionControlLineContent(line))
                .ToArray();
        });
    }

    public int LineCount => _lines.Value.Count;

    public IEnumerator<ILineContent> GetEnumerator() => _lines.Value.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public ILineContent GetLine(int index)
    {
        IReadOnlyList<ILineContent> lines = _lines.Value;

        if (index >= lines.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        return lines[index];
    }

    public override string ToString() => _content.Value;
}
