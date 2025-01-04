using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using VirtualGardener.Client.Models;
using VirtualGardener.Client.Models.ServerSettings;
using VirtualGardener.Client.Services.Abstraction;
using VirtualGardener.Client.Utilities;
using VirtualGardener.Shared.Models;
using IResult = VirtualGardener.Client.Utilities.IResult;

namespace VirtualGardener.Client.Services;

public class VirtualGardenerApiService(IOptions<ServerSettings> serverSettings) : IVirtualGardenerApiService
{
    private readonly string _baseUrl = serverSettings.Value.BaseUrl;

    private HttpClient CreateHttpClient() => new HttpClient();

    public async Task<IResult> RegisterAsync(User user)
    {
        using var client = CreateHttpClient();

        try
        {
            var url = $"{_baseUrl}auth/register";
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Result>(responseBody);

            return parsedResponse?.IsFullSuccess() == true
                ? parsedResponse
                : Result.Warning(parsedResponse?.StatusCode == ResultStatusCode.DataAlreadyExist
                    ? ResultStatusCode.DataAlreadyExist
                    : ResultStatusCode.UserCreationFailed);
        }
        catch (Exception ex)
        {
            return Result.Error(ResultStatusCode.Unknown, ex.Message);
        }
    }

    public async Task<IResult<User>> LogInAsync(string email, string password)
    {
        using var client = CreateHttpClient();

        try
        {
            var url = $"{_baseUrl}auth/logIn";
            var loginRequest = new LoginRequest { Email = email, Password = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Result<User>>(responseBody);

            return parsedResponse?.IsFullSuccess() == true
                ? parsedResponse
                : Result<User>.Warning(ResultStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            return Result<User>.Error(ResultStatusCode.Unknown, ex.Message);
        }
    }

    public async Task<IResult<List<Plant>>> GetMyPlantsAsync(Guid userId)
    {
        using var client = CreateHttpClient();

        try
        {
            var url = $"{_baseUrl}plant/getPlants/{userId}";

            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Result<List<Plant>>>(responseBody);

            return parsedResponse?.IsFullSuccess() == true
                ? parsedResponse
                : Result<List<Plant>>.Warning(ResultStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            return Result<List<Plant>>.Error(ResultStatusCode.Unknown, ex.Message);
        }
    }

    public async Task<IResult<Plant>> GetPlantDetailsAsync(Guid userId, string plantId)
    {
        using var client = CreateHttpClient();

        try
        {
            var url = $"{_baseUrl}plant/getPlantDetails/{plantId}/{userId}";

            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Result<Plant>>(responseBody);

            return parsedResponse?.IsFullSuccess() == true
                ? parsedResponse
                : Result<Plant>.Warning(ResultStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            return Result<Plant>.Error(ResultStatusCode.Unknown, ex.Message);
        }
    }

    public async Task<IResult> AddPlantAsync(Guid userId, Plant plant)
    {
        using var client = CreateHttpClient();

        try
        {
            var url = $"{_baseUrl}plant/add/{userId}";
            var content = new StringContent(JsonConvert.SerializeObject(plant), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Result>(responseBody);

            return parsedResponse?.IsFullSuccess() == true
                ? parsedResponse
                : Result.Warning(ResultStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            return Result.Error(ResultStatusCode.Unknown, ex.Message);
        }
    }

    public async Task<IResult> AddCareTaskAsync(Guid userId, string plantId, CareTask careTask)
    {
        using var client = CreateHttpClient();

        try
        {
            var url = $"{_baseUrl}plant/addCareTask/{plantId}/{userId}";
            var content = new StringContent(JsonConvert.SerializeObject(careTask), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Result>(responseBody);

            return parsedResponse?.IsFullSuccess() == true
                ? parsedResponse
                : Result.Warning(ResultStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            return Result.Error(ResultStatusCode.Unknown, ex.Message);
        }
    }

    public async Task<IResult> DeletePlantAsync(Guid userId, string plantId)
    {
        using var client = CreateHttpClient();

        try
        {
            var url = $"{_baseUrl}plant/deletePlant/{plantId}/{userId}";

            var response = await client.DeleteAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Result>(responseBody);

            return parsedResponse?.IsFullSuccess() == true
                ? parsedResponse
                : Result.Warning(ResultStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            return Result.Error(ResultStatusCode.Unknown, ex.Message);
        }
    }
}
