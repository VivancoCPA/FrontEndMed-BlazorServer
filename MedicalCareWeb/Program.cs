using MedicalCareWeb.Common;
using MedicalCareWeb.Components;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Services;
using MedicalCarweb.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]!;

if (string.IsNullOrWhiteSpace(apiBaseUrl))
    throw new InvalidOperationException(
        "ApiSettings:BaseUrl no encontrada en appsettings.json del Client");

// Registro correcto con AddHttpClient
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

//builder.Services.AddHttpClient("AppApi", client =>
//{
//    client.BaseAddress = new Uri(apiBaseUrl);
//});
// Cliente para APIs externas (sin BaseAddress fija)
builder.Services.AddHttpClient("Externo");

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IAseguradoraService, AseguradoraService> ();
builder.Services.AddScoped<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();

// Add MudBlazor services
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
