using Microsoft.AspNetCore.Components;
using VirtualGardener.Client.Models;
using VirtualGardener.Client.Services;
using VirtualGardener.Client.Services.Abstraction;

namespace VirtualGardener.Client.Components.Pages;

public partial class LoginPage
{
    [Inject] private IVirtualGardenerApiService _virtualGardenerApiService { get; set; }
    [Inject] private IVirtualGardenerLocalStorageService VirtualGardenerLocalStorageService { get; set; }
    [Inject] private NavigationManager _navigationManager { get; set; }

    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _errorMessage = string.Empty;

    private bool _isBusy = false;

    private async Task LoginAsync()
    {
        _errorMessage = string.Empty;
        _isBusy = true;
        if (!(string.IsNullOrWhiteSpace(_email) || string.IsNullOrWhiteSpace(_password)))
        {
            var result = await _virtualGardenerApiService.LogInAsync(_email, _password);
            if (result.IsFullSuccess())
            {
                var user = result.Data;

                await VirtualGardenerLocalStorageService.SetUserAuthStateAsync(new UserAuthState
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Role = user.Role
                });

                _navigationManager.NavigateTo("/");
            }
            else
                _errorMessage = "Login failed!";
        }

        _isBusy = false;
    }
}