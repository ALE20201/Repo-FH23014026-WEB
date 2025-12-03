global using Xunit;

using Transportation.Interfaces;
using Transportation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddKeyedSingleton<IAirplanes, Airbus>("airbus");
builder.Services.AddKeyedSingleton<IAirplanes, Boeing>("boeing");//ChatGPT

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
