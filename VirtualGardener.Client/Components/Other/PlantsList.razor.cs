using Microsoft.AspNetCore.Components;
using VirtualGardener.Client.Models;
using VirtualGardener.Client.Services.Abstraction;
using VirtualGardener.Shared.Models;

namespace VirtualGardener.Client.Components.Other;

public partial class PlantsList : ComponentBase
{
    [Parameter] public required List<Plant> PlantsSource { get; init; }
    [Parameter] public required UserAuthState UserAuthState { get; init; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IVirtualGardenerApiService VirtualGardenerApiService { get; set; }

    private void GoToDetails(Plant plant)
    {
        NavigationManager.NavigateTo($"/PlantDetails/{plant.Id}");
    }

    private async Task DeletePlantAsync(Plant plant)
    {
        await VirtualGardenerApiService.DeletePlantAsync(UserAuthState.Id, plant.Id.ToString());
        NavigationManager.NavigateTo($"/MyPlants/", true);
    }

    private string FormatNullableDate(DateTime? date)
    {
        return date.HasValue ? date.Value.ToShortDateString() : "No data";
    }
}