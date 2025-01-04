using Microsoft.AspNetCore.Components;
using VirtualGardener.Client.Models;
using VirtualGardener.Client.Utilities;
using VirtualGardener.Shared.Models;

namespace VirtualGardener.Client.Components.Pages;

public partial class PlantDetails : BaseAuthorizedPage
{
    [Parameter] public string? PlantId { get; set; }

    private bool _isWateringRequired;
    private bool _isFertilizingRequired;

    private Plant? _plant;

    private CareTask _careTaskToAdd = new()
    {
        TaskDate = DateTime.UtcNow,
    };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (IsAuthorized)
            {
                var result = await VirtualGardenerApiService.GetPlantDetailsAsync(UserAuthState.Id, PlantId);

                if (result.IsFullSuccess())
                    _plant = result.Data;

                else
                    _plant = null;


                CheckWatering();
                CheckPestControl();
                await InvokeAsync(StateHasChanged);
            }
        }
    }

    private async Task AddCareTask()
    {
        var result = await VirtualGardenerApiService.AddCareTaskAsync(UserAuthState.Id, PlantId, _careTaskToAdd);

        if (result.IsFullSuccess())
        {
            await InvokeAsync(StateHasChanged);
            _careTaskToAdd = new CareTask();
        }
    }

    private void CheckWatering()
    {
        if (_plant?.LastWatering is not null)
        {
            if (_plant.LastWatering!.Value.AddDays(Helpers.GetDaysByFrequency(_plant.WateringFrequency)) < DateTime.UtcNow)
                _isWateringRequired = true;
            else
                _isWateringRequired = false;
        }
    }

    private void CheckPestControl()
    {
        if (_plant?.LastFertilizing is not null)
        {
            if (_plant.LastFertilizing!.Value.AddDays(Helpers.GetDaysByFrequency(_plant.FertilizingFrequency)) < DateTime.UtcNow)
                _isFertilizingRequired = true;
            else
                _isFertilizingRequired = false;
        }
    }
}