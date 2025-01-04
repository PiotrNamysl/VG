using Microsoft.EntityFrameworkCore;
using Polly;
using VirtualGardenerServer.Database;
using VirtualGardenerServer.Models.ServerSettings;
using VirtualGardenerServer.Services;
using VirtualGardenerServer.Services.Abstraction;

var builder = WebApplication.CreateBuilder(args);
var serverSettings = builder.Configuration.GetSection("ServerSettings").Get<ServerSettings>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(serverSettings?.ConnectionString));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPlantService, PlantService>();

var app = builder.Build();

#if !DEBUG

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(15),
            (exception, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"Retry {retryCount} due to {exception.Message}. Waiting {timeSpan} before retry.");
            });

    try
    {
        var dbContext = services.GetRequiredService<DataContext>();
            retryPolicy.Execute(() =>
            {
                Console.WriteLine("Attempting database migration...");
                dbContext.Database.Migrate();
                Console.WriteLine("Database migration completed successfully.");
            });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration failed after retries. Exception: {ex.Message}");
        throw;
    }
}

#endif

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();