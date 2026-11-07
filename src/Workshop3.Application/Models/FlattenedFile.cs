using Workshop3.Application.FileSystem;

namespace Workshop3.Application.Models;

public sealed record FlattenedFile(
    string FullPath,
    IFileNode File);
