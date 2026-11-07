namespace Workshop3.Presentation.Abstractions.Keyboard;

public interface IKeyboardEventHandler
{
    Task OnKeyDownAsync(KeyDownEvent evt);
}
