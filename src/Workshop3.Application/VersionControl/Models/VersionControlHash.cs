namespace Workshop3.Application.VersionControl.Models;

public readonly record struct VersionControlHash(string Value)
{
    public bool Equals(VersionControlHash other)
        => Value.Equals(other.Value, StringComparison.Ordinal);

    public override int GetHashCode()
        => Value.GetHashCode();
}
