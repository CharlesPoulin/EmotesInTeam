using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using EmotesForTeam;
using EmotesForTeam.Services;
using Blazored.LocalStorage;
using EmotesForTeam.Pages.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5168/")
});
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddMudServices();
builder.Services.AddScoped<CardService>();

await builder.Build().RunAsync();
