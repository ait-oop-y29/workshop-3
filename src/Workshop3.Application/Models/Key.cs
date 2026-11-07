namespace Workshop3.Application.Models;

public readonly record struct Key(string Value)
{
    public int Length => Value.Length;
    
    public bool Is(string key) => Value.Equals(key, StringComparison.OrdinalIgnoreCase);
}
