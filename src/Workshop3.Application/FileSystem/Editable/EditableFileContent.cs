using System.Collections;
using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.FileSystem.Editable;

public sealed class EditableFileContent(
    IEnumerable<string> lines)
    : IEditableFileContent
{
    private readonly List<EditableLineContent> _lines = lines.Select(line => new EditableLineContent(line)).ToList();

    public int LineCount => _lines.Count;

    public IEditableLineContent GetEditableLine(int index)
    {
        if (index >= LineCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        return _lines[index];
    }

    public void InsertLine(int index, string line)
    {
        _lines.Insert(index, new EditableLineContent(line));
    }

    public void RemoveLine(int index)
    {
        _lines.RemoveAt(index);
    }

    public IEnumerator<ILineContent> GetEnumerator() => _lines.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString()
    {
        return string.Join("\n", _lines);
    }
}
