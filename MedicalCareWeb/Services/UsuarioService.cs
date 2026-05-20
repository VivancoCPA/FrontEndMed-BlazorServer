using MedicalCareWeb.Common;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Models.Aseguradora;
using MedicalCareWeb.Models.Usuario;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MedicalCareWeb.Services;

public class UsuarioService(
    HttpClient _http,
    CustomAuthStateProvider _authProvider) : BaseService(_http), IUsuarioService
{
    public async Task<PaginatedResultDto<UsuarioDto>> GetUsuarioPaged(ListedPagedDto paginacion)
    {
        var url = $"auth/users/paged?page={paginacion.Page}&pageSize={paginacion.PageSize}";

        if (!string.IsNullOrWhiteSpace(paginacion.Search))
            url += $"&search={Uri.EscapeDataString(paginacion.Search)}";

        if (!string.IsNullOrWhiteSpace(paginacion.SortBy))
            url += $"&sortBy={paginacion.SortBy}&sortDesc={paginacion.SortDesc.ToString().ToLower()}";

        //Console.WriteLine(url);
        var response = await _http.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<PaginatedResultDto<UsuarioDto>>();
            return data ?? new PaginatedResultDto<UsuarioDto>(
                Items: [],
                Page: paginacion.Page,
                PageSize: paginacion.PageSize,
                TotalCount: 0,
                Success: true,
                ErrorMessage: "",
                Errores: []);
        }

        //var error = await Helper.LeerErrorAsync(response);
        var errores = await ExtraerErrores(response);
        return new PaginatedResultDto<UsuarioDto>(
            Items: [],
            TotalCount: 0,
            Page: 0,
            PageSize: 0,
            Success: false,
            Errores: errores);
    }

    public async Task<(bool exitoso, List<string> errores)> Login(LoginDto request)
    {
        var response = await _http.PostAsJsonAsync("auth/login", request);
        if (response.IsSuccessStatusCode)
        {
            // 2. Deserializa el cuerpo directamente al tipo esperado
            var resultado = await response.Content.ReadFromJsonAsync<UsuarioResponseDto>();
            if (resultado is null)
            {
                return (false, ["Respuesta inválida del servidor."]);
            }

            // ✅ Notifica a Blazor que el usuario está autenticado
            await _authProvider.MarcarUsuarioAutenticado(resultado.token);
            return (true, []);
        }
        // ✅ Extrae y formatea los errores automáticamente
        var errores = await ExtraerErrores(response);
        return (false, errores);

    }
    public async Task Logout()
    {
        await _authProvider.MarcarUsuarioDesautenticado();
    }

    public async Task<(bool exitoso, List<string> errores)> ToggleUsuarioAsync(Guid id)
    {
        var response = await _http.PatchAsync($"users/{id}/toggle-status", content: null);
        if (response.IsSuccessStatusCode)
            //var data = await response.Content.ReadFromJsonAsync<bool>();
            return (true, []);
        // ❌ leer error backend
        //var error = await Helper.LeerErrorAsync(response);
        var errores = await ExtraerErrores(response);
        return (false, errores);
    }
}
