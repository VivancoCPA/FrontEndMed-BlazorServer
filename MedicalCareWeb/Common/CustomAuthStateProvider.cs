namespace MedicalCareWeb.Common;

// Auth/CustomAuthStateProvider.cs
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class CustomAuthStateProvider(
    ILocalStorageService _localStorage,
    HttpClient _http
) : AuthenticationStateProvider
{
    private readonly AuthenticationState _anonimo =
        new(new ClaimsPrincipal(new ClaimsIdentity()));

    // ✅ Flag que indica si el browser ya está listo
    private bool _browserListo = false;
    private string? _tokenEnMemoria = null;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // ✅ Si el browser no está listo aún (prerender), retorna anónimo sin tocar JS
        if (!_browserListo)
            return _anonimo;

        try
        {
            var token = _tokenEnMemoria
                ?? await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
                return _anonimo;

            if (TokenExpirado(token))
            {
                await LimpiarSesion();
                return _anonimo;
            }

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var claims = ParsearClaimsDelToken(token);
            var identity = new ClaimsIdentity(claims, "jwt");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch (InvalidOperationException)
        {
            // JS aún no disponible — retorna anónimo de forma segura
            return _anonimo;
        }
    }

    // ✅ Llamar este método desde OnAfterRenderAsync en el componente raíz
    public async Task InicializarDesdeStorage()
    {
        _browserListo = true;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarcarUsuarioAutenticado(string token)
    {
        _tokenEnMemoria = token;
        _browserListo = true;

        await _localStorage.SetItemAsync("authToken", token);

        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var claims = ParsearClaimsDelToken(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var usuario = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(usuario)));
    }

    public async Task MarcarUsuarioDesautenticado()
    {
        _tokenEnMemoria = null;
        await LimpiarSesion();
        NotifyAuthenticationStateChanged(Task.FromResult(_anonimo));
    }

    private async Task LimpiarSesion()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _http.DefaultRequestHeaders.Authorization = null;
    }

    private static IEnumerable<Claim> ParsearClaimsDelToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.Claims;
    }

    private static bool TokenExpirado(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.ValidTo < DateTime.UtcNow;
    }
}