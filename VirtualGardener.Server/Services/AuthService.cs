using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;
using VirtualGardener.Shared.Models.Enums;
using VirtualGardenerServer.Database;
using VirtualGardenerServer.Models;
using VirtualGardenerServer.Services.Abstraction;
using VirtualGardenerServer.Utilities;
using IResult = VirtualGardenerServer.Utilities.IResult;

namespace VirtualGardenerServer.Services;

public class AuthService(DataContext dataContext) : IAuthService
{
    public async Task<IResult> RegisterAsync(User user)
    {
        try
        {
            var tryToGetUser = await dataContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (tryToGetUser is not null)
                return Result.Success(ResultStatusCode.DataAlreadyExist);

            var userEntity = await dataContext.Users.AddAsync(new UserEntity
            {
                Email = user.Email,
                Name = user.Name,
                Password = GetHashedPassword(user.Password),
                Role = Role.User
            });

            await dataContext.SaveChangesAsync();

            return Result.Success(ResultStatusCode.Ok);
        }
        catch (Exception ex)
        {
            return Result.Error(ResultStatusCode.DatabaseError, ex.Message);
        }
    }

    public async Task<IResult<UserDto>> SignInAsync(string email, string password)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user is not null)
            return user.Password == GetHashedPassword(password)
                ? Result<UserDto>.Success(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Role = user.Role
                })
                : Result<UserDto>.Warning(ResultStatusCode.AccessForbidden);

        return Result<UserDto>.Success(ResultStatusCode.NoDataFound);
    }

    private static string GetHashedPassword(string password)
        => Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(password))).ToLower();
}