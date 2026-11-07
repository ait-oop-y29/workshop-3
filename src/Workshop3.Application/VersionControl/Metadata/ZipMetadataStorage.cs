using System.IO.Compression;
using System.Text.Json;
using Workshop3.Application.VersionControl.Models;

namespace Workshop3.Application.VersionControl.Metadata;

public sealed class ZipMetadataStorage(
    ZipArchive archive,
    string path)
    : IVersionControlMetadataStorage
{
    private VersionControlMetadata? _metadata;

    public VersionControlHash? GetHeadCommitHash()
    {
        VersionControlMetadata metadata = LoadMetadata();
        return metadata.HeadHash;
    }

    public void UpdateHeadCommitHash(VersionControlHash hash)
    {
        VersionControlMetadata metadata = LoadMetadata();
        metadata = metadata with { HeadHash = hash };

        SaveMetadata(metadata);
    }

    private VersionControlMetadata LoadMetadata()
    {
        if (_metadata is not null)
            return _metadata;

        ZipArchiveEntry? entry = archive.GetEntry(path);

        if (entry is null)
        {
            _metadata = new VersionControlMetadata(HeadHash: null);
            return _metadata;
        }

        using Stream stream = entry.Open();

        _metadata = JsonSerializer.Deserialize<VersionControlMetadata>(stream)
                    ?? new VersionControlMetadata(HeadHash: null);

        return _metadata;
    }

    private void SaveMetadata(VersionControlMetadata metadata)
    {
        _metadata = metadata;

        ZipArchiveEntry entry = archive.GetEntry(path) ?? archive.CreateEntry(path, CompressionLevel.Fastest);
        using Stream stream = entry.Open();

        JsonSerializer.Serialize(stream, _metadata);
    }
}
