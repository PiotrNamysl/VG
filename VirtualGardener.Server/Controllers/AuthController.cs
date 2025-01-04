using Microsoft.AspNetCore.Mvc;
using VirtualGardener.Shared.Models.Requests;
using VirtualGardenerServer.Models;
using VirtualGardenerServer.Services.Abstraction;
using VirtualGardenerServer.Utilities;
using IResult = VirtualGardenerServer.Utilities.IResult;

namespace VirtualGardenerServer.Controllers;

public class AuthController(IAuthService authService) : BaseController
{
    [HttpPost("register")]
    public async Task<IResult> RegisterAsync(User user)
    {
        var result = await authService.RegisterAsync(user);

        return result;
    }

    [HttpPost("logIn")]
    public async Task<IResult<UserDto>> SignInAsync(LogInRequest request)
    {
        var result = await authService.SignInAsync(request.Email, request.Password);

        return result;
    }
}