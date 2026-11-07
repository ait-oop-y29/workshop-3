using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using Workshop3.Application.VersionControl.Models;

namespace Workshop3.Application.VersionControl.ObjectStorage;

public sealed class ZipObjectStorage(
    ZipArchive archive,
    string objectStoragePath)
    : IVersionControlObjectStorage
{
    public void CreateObject(
        VersionControlHash hash,
        string content)
    {
        string objectPath = Path.Combine(objectStoragePath, hash.Value);

        ZipArchiveEntry entry = archive.CreateEntry(objectPath, CompressionLevel.Fastest);
        using Stream stream = entry.Open();

        using var writer = new StreamWriter(stream, leaveOpen: true);
        writer.Write(content);
    }

    public bool TryReadObject(VersionControlHash hash, [NotNullWhen(true)] out string? content)
    {
        string objectPath = Path.Combine(objectStoragePath, hash.Value);

        ZipArchiveEntry? entry = archive.GetEntry(objectPath);

        if (entry is null)
        {
            content = null;
            return false;
        }

        using Stream stream = entry.Open();
        using var reader = new StreamReader(stream, leaveOpen: true);

        content = reader.ReadToEnd();
        return true;
    }
}
