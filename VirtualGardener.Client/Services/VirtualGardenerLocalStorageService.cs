using VirtualGardener.Client.Models;
using VirtualGardener.Client.Services.Abstraction;

namespace VirtualGardener.Client.Services;

public class VirtualGardenerLocalStorageService(Blazored.LocalStorage.ILocalStorageService localStorageService) : IVirtualGardenerLocalStorageService
{
    private const string UserAuthStateKey = "UserAuthState";

    public async Task<UserAuthState?> GetUserAuthStateAsync()
        => await localStorageService.GetItemAsync<UserAuthState?>(UserAuthStateKey);

    public async Task SetUserAuthStateAsync(UserAuthState userAuthState)
        => await localStorageService.SetItemAsync(UserAuthStateKey, userAuthState);

    public async Task ClearUserAuthState()
        => await localStorageService.RemoveItemAsync(UserAuthStateKey);
}