using Microsoft.AspNetCore.Components;
using VirtualGardener.Client.Models;
using VirtualGardener.Client.Services;
using VirtualGardener.Client.Services.Abstraction;

namespace VirtualGardener.Client.Components.Pages;

public class BaseAuthorizedPage : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; init; }
    [Inject] public required IVirtualGardenerLocalStorageService VirtualGardenerLocalStorageService { get; init; }
    [Inject] public required IVirtualGardenerApiService VirtualGardenerApiService { get; init; }
    protected UserAuthState? UserAuthState { get; set; }
    private string? UserName => UserAuthState?.Name;
    protected bool IsAuthorized => UserName is not null;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            UserAuthState = await VirtualGardenerLocalStorageService.GetUserAuthStateAsync();

            await InvokeAsync(StateHasChanged);
        }
    }
}