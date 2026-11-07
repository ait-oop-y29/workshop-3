using Microsoft.AspNetCore.Components;

namespace Workshop3.Presentation.Pages.Tabs;

public sealed record TabDescriptor(
    string DisplayName,
    string IconName,
    RenderFragment RenderFragment);
