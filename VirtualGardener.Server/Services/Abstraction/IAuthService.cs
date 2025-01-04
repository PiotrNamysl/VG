using VirtualGardenerServer.Models;
using VirtualGardenerServer.Utilities;
using IResult = VirtualGardenerServer.Utilities.IResult;

namespace VirtualGardenerServer.Services.Abstraction;

public interface IAuthService
{
    public Task<IResult> RegisterAsync(User user);
    public Task<IResult<UserDto>> SignInAsync(string email, string password);
}