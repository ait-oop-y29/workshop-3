namespace Workshop3.Application.Models;

public abstract record HandleKeyResult
{
    private HandleKeyResult() { }

    public sealed record Success : HandleKeyResult;

    public sealed record Failure(string ErrorMessage) : HandleKeyResult;
}
