namespace Workshop3.Application.FileSystem.Content;

public interface IEditableFileContent : IFileContent
{
    ILineContent IFileContent.GetLine(int index) => GetEditableLine(index);

    IEditableLineContent GetEditableLine(int index);

    void InsertLine(int index, string line);

    void RemoveLine(int index);
}
