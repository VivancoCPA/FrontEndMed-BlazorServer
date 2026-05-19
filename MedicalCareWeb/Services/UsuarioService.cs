using MedicalCareWeb.Common;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Models.Usuario;
using static System.Net.WebRequestMethods;


namespace MedicalCareWeb.Services;

public class UsuarioService(
    HttpClient _http,
    CustomAuthStateProvider _authProvider) : BaseService(_http), IUsuarioService
{
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
}
