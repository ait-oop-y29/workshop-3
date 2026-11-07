using System.Diagnostics.CodeAnalysis;
using Workshop3.Application.VersionControl.Models;

namespace Workshop3.Application.VersionControl.ObjectStorage;

public interface IVersionControlObjectStorage
{
    void CreateObject(
        VersionControlHash hash,
        string content);

    bool TryReadObject(
        VersionControlHash hash,
        [NotNullWhen(true)] out string? content);
}
