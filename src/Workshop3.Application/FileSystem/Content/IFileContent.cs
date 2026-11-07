namespace Workshop3.Application.FileSystem.Content;

public interface IFileContent : IEnumerable<ILineContent>
{
    int LineCount { get; }

    ILineContent GetLine(int index);
    
    string ToString();
}
