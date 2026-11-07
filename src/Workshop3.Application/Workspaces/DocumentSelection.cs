namespace Workshop3.Application.Workspaces;

public readonly record struct DocumentSelection
{
    public static readonly DocumentSelection Default = new(
        DocumentPosition.Default,
        DocumentPosition.Default);

    private readonly DocumentPosition _min;
    private readonly DocumentPosition _max;

    public DocumentSelection(DocumentPosition start, DocumentPosition end)
    {
        Start = start;
        End = end;

        _min = DocumentPosition.Min(Start, End);
        _max = DocumentPosition.Max(Start, End);
    }

    public DocumentPosition Start { get; }
    public DocumentPosition End { get; }

    public bool Contains(DocumentPosition position)
    {
        if (position.Line < _min.Line || position.Line > _max.Line)
            return false;

        if (_min.Line < position.Line && position.Line < _max.Line)
            return true;

        if (_min.Line == _max.Line)
            return _min.Character <= position.Character && position.Character <= _max.Character;

        if (_min.Line == position.Line)
            return _min.Character <= position.Character;

        if (_max.Line == position.Line)
            return position.Character <= _max.Character;

        return false;
    }

    public DocumentSelection WithStart(DocumentPosition start)
    {
        return new DocumentSelection(start, End);
    }

    public DocumentSelection WithEnd(DocumentPosition end)
    {
        return new DocumentSelection(Start, end);
    }
}
