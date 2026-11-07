using Workshop3.Application.FileSystem;
using Workshop3.Application.FileSystem.Content;
using Workshop3.Application.Models;

namespace Workshop3.Application.Workspaces;

public sealed class Workspace(IFileSystemNode fileSystem)
{
    public IFileSystemNode FileSystem { get; } = fileSystem;

    public IFileNode? SelectedFile { get; private set; }

    public EditorMode Mode { get; private set; }

    public DocumentSelection Selection { get; private set; } = DocumentSelection.Default;

    public void ChangeSelectedFile(IFileNode? file)
    {
        SelectedFile = file;
        Mode = EditorMode.Normal;
        Selection = DocumentSelection.Default;
    }

    public void OnKeyPressed(Key key)
    {
        if (key.Is("ArrowRight")
            || (key.Is("l") && Mode is not EditorMode.Insert))
        {
            MoveRight();
        }
        else if (key.Is("ArrowLeft")
                 || (key.Is("h") && Mode is not EditorMode.Insert))
        {
            MoveLeft();
        }
        else if (key.Is("ArrowDown")
                 || (key.Is("j") && Mode is not EditorMode.Insert))
        {
            MoveDown();
        }
        else if (key.Is("ArrowUp")
                 || (key.Is("k") && Mode is not EditorMode.Insert))
        {
            MoveUp();
        }
        else if (key.Is("v") && Mode is EditorMode.Normal)
        {
            Mode = EditorMode.Visual;
        }
        else if (key.Is("i") && Mode is EditorMode.Normal)
        {
            Mode = EditorMode.Insert;
        }
        else if ((key.Is("Escape") && Mode is not EditorMode.Normal)
                 || (key.Is("v") && Mode is EditorMode.Visual))
        {
            Mode = EditorMode.Normal;
            Selection = Selection.WithStart(Selection.End);
        }
        else if (Mode is EditorMode.Insert && (key.Is("Return") || key.Is("Enter")))
        {
            InsertNewLine();
        }
        else if (Mode is EditorMode.Insert && key.Length is 1)
        {
            InsertCharacter(key);
        }
        else if (Mode is EditorMode.Insert && key.Is("Backspace"))
        {
            DeleteCharacter();
        }
    }

    private void MoveRight()
    {
        if (SelectedFile is null)
            return;

        DocumentPosition end = Selection.End;

        IFileContent content = SelectedFile.GetContent();
        ILineContent line = content.GetLine(end.Line);

        if (line.Length >= end.Character + 1)
        {
            end = end with { Character = end.Character + 1 };
        }
        else if (content.LineCount > end.Line)
        {
            end = new DocumentPosition(Line: end.Line + 1, Character: 0);
        }

        MoveTo(end);
    }

    private void MoveLeft()
    {
        if (SelectedFile is null)
            return;

        DocumentPosition end = Selection.End;

        IFileContent content = SelectedFile.GetContent();

        if (end.Character > 0)
        {
            end = end with { Character = end.Character - 1 };
        }
        else if (end.Line > 0)
        {
            ILineContent previous = content.GetLine(end.Line - 1);
            end = new DocumentPosition(Line: end.Line - 1, Character: previous.Length - 1);
        }

        MoveTo(end);
    }

    private void MoveUp()
    {
        if (SelectedFile is null)
            return;

        DocumentPosition end = Selection.End;

        IFileContent content = SelectedFile.GetContent();

        if (end.Line > 0)
        {
            ILineContent previous = content.GetLine(end.Line - 1);

            end = new DocumentPosition(
                Line: end.Line - 1,
                Character: Math.Min(end.Character, previous.Length));
        }
        else
        {
            end = end with { Character = 0 };
        }

        MoveTo(end);
    }

    private void MoveDown()
    {
        if (SelectedFile is null)
            return;

        DocumentPosition end = Selection.End;

        IFileContent content = SelectedFile.GetContent();

        if (end.Line < content.LineCount - 1)
        {
            ILineContent next = content.GetLine(end.Line + 1);

            end = new DocumentPosition(
                Line: end.Line + 1,
                Character: Math.Min(end.Character, next.Length));
        }
        else
        {
            ILineContent line = content.GetLine(end.Line);
            end = end with { Character = line.Length };
        }

        MoveTo(end);
    }

    private void InsertNewLine()
    {
        if (SelectedFile is null
            || SelectedFile.TryGetEditableContent(out IEditableFileContent? content) is false)
        {
            return;
        }

        DocumentPosition position = Selection.End;

        IEditableLineContent line = content.GetEditableLine(position.Line);
        var lineContent = line.ToString();

        string remainer = lineContent.Length > position.Character
            ? lineContent[position.Character..]
            : string.Empty;

        if (remainer.Length > 0)
        {
            line.RemoveAt(position.Character, length: remainer.Length);
        }

        content.InsertLine(position.Line + 1, remainer);
        MoveTo(new DocumentPosition(position.Line + 1, 0));
    }

    private void InsertCharacter(Key key)
    {
        if (SelectedFile is null
            || SelectedFile.TryGetEditableContent(out IEditableFileContent? content) is false)
        {
            return;
        }

        DocumentPosition position = Selection.End;

        IEditableLineContent line = content.GetEditableLine(position.Line);
        line.InsertAt(position.Character, key.Value);

        position = position with { Character = position.Character + 1 };
        MoveTo(position);
    }

    private void DeleteCharacter()
    {
        if (SelectedFile is null
            || SelectedFile.TryGetEditableContent(out IEditableFileContent? content) is false)
            return;

        DocumentPosition position = Selection.End;
        IEditableLineContent line = content.GetEditableLine(position.Line);

        if (position.Character > 0)
        {
            line.RemoveAt(position.Character - 1);

            position = position with { Character = position.Character - 1 };
            MoveTo(position);
        }
        else if (position.Line > 0)
        {
            IEditableLineContent previousLine = content.GetEditableLine(position.Line - 1);
            int length = previousLine.Length;

            previousLine.InsertAt(length, line.ToString());
            content.RemoveLine(position.Line);

            position = new DocumentPosition(Line: position.Line - 1, Character: length);
            MoveTo(position);
        }
    }

    private void MoveTo(DocumentPosition end)
    {
        Selection = Mode switch
        {
            EditorMode.Normal or EditorMode.Insert => Selection.WithStart(end).WithEnd(end),
            EditorMode.Visual => Selection.WithEnd(end),
            _ => throw new ArgumentOutOfRangeException(nameof(end)),
        };
    }
}
