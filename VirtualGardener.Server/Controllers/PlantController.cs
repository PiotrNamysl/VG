using Microsoft.AspNetCore.Mvc;
using VirtualGardener.Shared.Models;
using VirtualGardenerServer.Models;
using VirtualGardenerServer.Services.Abstraction;
using VirtualGardenerServer.Utilities;
using IResult = VirtualGardenerServer.Utilities.IResult;

namespace VirtualGardenerServer.Controllers;

public class PlantController(IPlantService plantService) : BaseController
{
    [HttpGet("getPlants/{userId}")]
    public async Task<IResult<List<Plant>>> GetPlantsAsync(Guid userId) => await plantService.GetPlantsAsync(userId);

    [HttpGet("getPlantDetails/{plantId}/{userId}")]
    public async Task<IResult<Plant>> GetPlantDetailsAsync(Guid userId, string plantId) =>
        await plantService.GetPlantDetailsAsync(userId, Guid.Parse(plantId));

    [HttpPost("add/{userId}")]
    public async Task<IResult> AddPlantAsync(Guid userId, [FromBody] AddPlantRequest plant) => await plantService.AddPlantAsync(userId, plant);

    [HttpPost("addCareTask/{plantId}/{userId}")]
    public async Task<IResult> AddCareTaskAsync(Guid userId, string plantId, [FromBody] CareTask careTask) =>
        await plantService.AddCareTaskAsync(userId, Guid.Parse(plantId), careTask);

    [HttpDelete("deletePlant/{plantId}/{userId}")]
    public async Task<IResult> DeletePlantAsync(Guid userId, string plantId) =>
        await plantService.DeletePlantAsync(userId, Guid.Parse(plantId));
}