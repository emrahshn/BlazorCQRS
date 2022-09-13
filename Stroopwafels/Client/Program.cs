using Application;
using Application.Factories;
using Application.Services;
using Client.Infrastructure.Managers.Orders;
using Client.Infrastructure.Managers.Quote;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Stroopwafels.Client;
using Stroopwafels.Shared.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IStroopwafelSupplierProviderFactory, StroopwafelSupplierProviderFactory>();
builder.Services.AddScoped<IStroopwafelsApiService, StroopwafelsApiService>();
builder.Services.AddTransient<IQuoteManager, QuoteManager>();
builder.Services.AddTransient<IOrdersManager, OrdersManager>();

await builder.Build().RunAsync();
