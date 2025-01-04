using VirtualGardener.Client.Components.Other;
using VirtualGardener.Client.Models;
using VirtualGardener.Shared.Models;

namespace VirtualGardener.Client.Components.Pages;

public partial class MyPlantsPage : BaseAuthorizedPage
{
    private List<Plant> _myPlants = [];
    private PlantCreator _plantCreator;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await RefreshPlants();
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task RefreshPlants()
    {
        if (UserAuthState is not null)
        {
            var result = await VirtualGardenerApiService.GetMyPlantsAsync(UserAuthState.Id);
            if (result.IsFullSuccess())
            {
                _myPlants.Clear();
                _myPlants.AddRange(result.Data);
            }
        }
    }
}