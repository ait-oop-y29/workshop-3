using Workshop3.Application.FileSystem;
using Workshop3.Application.FileSystem.Content;
using Workshop3.Application.Models;

namespace Workshop3.Application.Diff;

public sealed class FileTreeDiffFactory
{
    public IEnumerable<FileNodeDiff> GenerateDiff(
        IFileSystemNode old,
        IFileSystemNode current)
    {
        var oldFiles = new List<FlattenedFile>();
        var currentFiles = new List<FlattenedFile>();

        Flatten(old, string.Empty, oldFiles);
        Flatten(current, string.Empty, currentFiles);

        using List<FlattenedFile>.Enumerator oldEnumerator = oldFiles.GetEnumerator();
        using List<FlattenedFile>.Enumerator currentEnumerator = currentFiles.GetEnumerator();

        bool hasOld = oldEnumerator.MoveNext();
        bool hasCurrent = currentEnumerator.MoveNext();

        while (hasOld || hasCurrent)
        {
            if (hasOld is false)
            {
                yield return new FileNodeDiff.Added(
                    currentEnumerator.Current.FullPath,
                    currentEnumerator.Current.File);

                hasCurrent = currentEnumerator.MoveNext();
            }
            else if (hasCurrent is false)
            {
                yield return new FileNodeDiff.Removed(
                    oldEnumerator.Current.FullPath,
                    oldEnumerator.Current.File);

                hasOld = oldEnumerator.MoveNext();
            }
            else
            {
                int pathComparison = string.Compare(
                    currentEnumerator.Current.FullPath,
                    oldEnumerator.Current.FullPath,
                    StringComparison.Ordinal);

                // current < old -> current file is additional, but there may be more files before old
                if (pathComparison < 0)
                {
                    yield return new FileNodeDiff.Added(
                        currentEnumerator.Current.FullPath,
                        currentEnumerator.Current.File);

                    hasCurrent = currentEnumerator.MoveNext();
                }
                // current > old -> passed old file not meeting it, old file is removed,
                // but there may be more old files removed
                else if (pathComparison > 0)
                {
                    yield return new FileNodeDiff.Removed(
                        oldEnumerator.Current.FullPath,
                        oldEnumerator.Current.File);

                    hasOld = oldEnumerator.MoveNext();
                }
                // current = old, but content may differ
                else
                {
                    IFileContent oldContent = oldEnumerator.Current.File.GetContent();
                    IFileContent currentContent = currentEnumerator.Current.File.GetContent();

                    if (oldContent.ToString().Equals(currentContent.ToString(), StringComparison.Ordinal) is false)
                    {
                        yield return new FileNodeDiff.Changed(
                            currentEnumerator.Current.FullPath,
                            oldEnumerator.Current.File,
                            currentEnumerator.Current.File);
                    }

                    hasOld = oldEnumerator.MoveNext();
                    hasCurrent = currentEnumerator.MoveNext();
                }
            }
        }
    }

    private void Flatten(
        IFileSystemNode node,
        string path,
        List<FlattenedFile> files)
    {
        if (node is IFileNode file)
        {
            files.Add(new FlattenedFile(Path.Combine(path, file.Name), file));
        }
        else if (node is IDirectoryNode directory)
        {
            string directoryPath = Path.Combine(path, directory.Name);

            foreach (IFileSystemNode child in directory)
            {
                Flatten(child, directoryPath, files);
            }
        }
    }
}
