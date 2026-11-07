namespace Workshop3.Application.Workspaces;

public readonly record struct DocumentPosition(int Line, int Character)
{
    public static readonly DocumentPosition Default = new(0, 0);

    public static DocumentPosition Min(DocumentPosition left, DocumentPosition right)
    {
        return (left, right) switch
        {
            _ when left.Line < right.Line => left,
            _ when left.Line > right.Line => right,
            _ when left.Character < right.Character => left,
            _ when left.Character > right.Character => right,
            _ => left,
        };
    }

    public static DocumentPosition Max(DocumentPosition left, DocumentPosition right)
    {
        return (left, right) switch
        {
            _ when left.Line < right.Line => right,
            _ when left.Line > right.Line => left,
            _ when left.Character < right.Character => right,
            _ when left.Character > right.Character => left,
            _ => right,
        };
    }
}
