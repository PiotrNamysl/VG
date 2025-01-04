using VirtualGardener.Client.Models;

namespace VirtualGardener.Client.Services.Abstraction;

public interface IVirtualGardenerLocalStorageService
{
    Task<UserAuthState?> GetUserAuthStateAsync();
    Task SetUserAuthStateAsync(UserAuthState userAuthState);
    Task ClearUserAuthState();
}