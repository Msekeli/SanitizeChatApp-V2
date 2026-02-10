using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SanitizeChatApp.Client;
using SanitizeChatApp.Client.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for API calls
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7276/")
});

// Register WordService here, BEFORE Build()
builder.Services.AddScoped<WordService>();

await builder.Build().RunAsync();

