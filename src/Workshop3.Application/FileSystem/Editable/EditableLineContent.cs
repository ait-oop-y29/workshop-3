using Workshop3.Application.FileSystem.Content;

namespace Workshop3.Application.FileSystem.Editable;

public sealed class EditableLineContent(
    string line) 
    : IEditableLineContent
{
    private string _line = line;

    public int Length => _line.Length;

    public void InsertAt(int index, string characters)
    {
        if (index >= _line.Length)
        {
            _line += characters;
        }
        else
        {
            _line = _line.Insert(index, characters);
        }
    }

    public void RemoveAt(int index, int length)
    {
        if (index < 0)
            return;

        if (index >= _line.Length)
        {
            _line = _line[..^2];
        }
        else
        {
            _line = _line.Remove(index, count: length);
        }
    }

    public override string ToString() => _line;
}
