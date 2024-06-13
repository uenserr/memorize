using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Memorize.Client;
using Memorize;
using Memorize.Client.Services.Interfaces;
using Memorize.Client.Services.Implementations;

using Microsoft.Extensions.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<LibraryService>();
builder.Services.AddScoped<DeckService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SearchService>();

await builder.Build().RunAsync();
