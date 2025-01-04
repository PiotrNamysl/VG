using Microsoft.AspNetCore.Components;
using VirtualGardener.Client.Models;
using VirtualGardener.Client.Services.Abstraction;

namespace VirtualGardener.Client.Components.Other;

public partial class NavBar : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IVirtualGardenerLocalStorageService VirtualGardenerLocalStorageService { get; set; }
    [Parameter] public UserAuthState? UserAuthState { get; set; }

    private bool _isAccountDetailsVisible = false;

    private async Task SignOutAsync()
    {
        await VirtualGardenerLocalStorageService.ClearUserAuthState();
        NavigationManager.NavigateTo("/logIn");
    }

    private void ChangeAccountDetailsVisibility()
    {
        _isAccountDetailsVisible = !_isAccountDetailsVisible;
    }
}