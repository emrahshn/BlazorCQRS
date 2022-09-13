using Application;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using Application.Services;
using Stroopwafels.Shared.Models;
using Application.Factories;
using Client.Infrastructure.Managers.Quote;
using Client.Infrastructure.Managers.Orders;

var builder = WebApplication.CreateBuilder(args);

//Add connection string to the container
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
var directory = Directory.GetCurrentDirectory() + "\\App_Data";
AppDomain.CurrentDomain.SetData("DataDirectory", directory);
builder.Services.AddDbContext<WafelsDbContext>(options =>
{
    options.UseSqlServer(connString);
});

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<ServiceInformation>(options => builder.Configuration.GetSection("ServiceInformation").Bind(options));
builder.Services.Configure<MailConfiguration>(options => builder.Configuration.GetSection("MailConfiguration").Bind(options));
builder.Services.AddOptions();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(ApplicationDbContext));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IStroopwafelsApiService, StroopwafelsApiService>();
builder.Services.AddScoped<IStroopwafelSupplierProviderFactory, StroopwafelSupplierProviderFactory>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddTransient<IQuoteManager, QuoteManager>();
builder.Services.AddTransient<IOrdersManager, OrdersManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
