using VirtualGardener.Client.Models;
using VirtualGardener.Client.Utilities;
using VirtualGardener.Shared.Models;
using IResult = VirtualGardener.Client.Utilities.IResult;

namespace VirtualGardener.Client.Services.Abstraction;

public interface IVirtualGardenerApiService
{
    Task<IResult> RegisterAsync(User user);
    Task<IResult<User>> LogInAsync(string email, string password);
    Task<IResult<List<Plant>>> GetMyPlantsAsync(Guid userId);
    Task<IResult<Plant>> GetPlantDetailsAsync(Guid userId, string plantId);
    Task<IResult> AddPlantAsync(Guid userId, Plant plant);
    Task<IResult> AddCareTaskAsync(Guid userId, string plantId, CareTask careTask);
    Task<IResult> DeletePlantAsync(Guid userId, string plantId);
}