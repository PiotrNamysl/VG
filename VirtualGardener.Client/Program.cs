using Blazored.LocalStorage;
using Microsoft.AspNetCore.DataProtection;
using Radzen;
using VirtualGardener.Client.Components;
using VirtualGardener.Client.Models.ServerSettings;
using VirtualGardener.Client.Services;
using VirtualGardener.Client.Services.Abstraction;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ServerSettings>(builder.Configuration.GetSection("ServerSettings"));
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<IVirtualGardenerApiService, VirtualGardenerApiService>();
builder.Services.AddScoped<IVirtualGardenerLocalStorageService, VirtualGardenerLocalStorageService>();
builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();