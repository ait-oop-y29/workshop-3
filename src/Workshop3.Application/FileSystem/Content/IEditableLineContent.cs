namespace Workshop3.Application.FileSystem.Content;

public interface IEditableLineContent : ILineContent
{
    void InsertAt(int index, string characters);

    void RemoveAt(int index, int length = 1);
}
