using VirtualGardener.Shared.Models;
using VirtualGardenerServer.Models;
using VirtualGardenerServer.Utilities;
using IResult = VirtualGardenerServer.Utilities.IResult;

namespace VirtualGardenerServer.Services.Abstraction;

public interface IPlantService
{
    Task<IResult<List<Plant>>> GetPlantsAsync(Guid userId);
    Task<IResult<Plant>> GetPlantDetailsAsync(Guid userId, Guid plantId);
    Task<IResult> AddPlantAsync(Guid userId, AddPlantRequest plant);
    Task<IResult> DeletePlantAsync(Guid userId, Guid plantId);
    Task<IResult> AddCareTaskAsync(Guid userId, Guid plantId, CareTask careTask);
}