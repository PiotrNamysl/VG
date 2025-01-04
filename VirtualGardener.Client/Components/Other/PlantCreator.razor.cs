using Microsoft.AspNetCore.Components;
using VirtualGardener.Client.Models;
using VirtualGardener.Client.Services.Abstraction;
using VirtualGardener.Shared.Models;

namespace VirtualGardener.Client.Components.Other;

public partial class PlantCreator : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IVirtualGardenerLocalStorageService VirtualGardenerLocalStorageService { get; set; }
    [Inject] public required IVirtualGardenerApiService VirtualGardenerApiService { get; set; }
    [Parameter] public required UserAuthState UserAuthState { get; set; }
    [Parameter] public EventCallback RefreshPlants { get; set; }

    private bool _isPopupVisible = false;
    private Plant _plant = new();
    private string? _errorMessage;

    private async Task AddPlantAsync()
    {
        var result = await VirtualGardenerApiService.AddPlantAsync(UserAuthState.Id, _plant);
        if (result.IsFullSuccess)
        {
            await RefreshPlants.InvokeAsync();
            await ClosePopup();
        }
    }

    public async Task ShowPopupAsync()
    {
        _isPopupVisible = true;
        await InvokeAsync(StateHasChanged);
    }

    public async Task ClosePopup()
    {
        _isPopupVisible = false;
        await InvokeAsync(StateHasChanged);
    }
}