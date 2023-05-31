using Blazored.Modal;
using Solita.Bike.App.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredModal();
builder.Services.AddHttpClient("BikeService", httpClient =>
{
    var connectionString = builder.Configuration.GetConnectionString("BikeService");
    httpClient.BaseAddress = new Uri(connectionString ?? throw new Exception("No connection string was given."));
});
builder.Services.AddSingleton<BikeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
