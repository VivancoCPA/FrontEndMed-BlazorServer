using System.Text.Json;

namespace MedicalCareWeb.Common;

// Services/BaseService.cs
public abstract class BaseService(HttpClient _http)
{
    protected async Task<List<string>> ExtraerErrores(HttpResponseMessage response)
    {
        var contenido = await response.Content.ReadAsStringAsync();

        try
        {
            var error = JsonSerializer.Deserialize<ApiErrorResponse>(contenido,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (error?.Errors?.Count > 0)
                return error.ObtenerErrores();

            // Si no tiene estructura de validación, usa el título
            if (!string.IsNullOrEmpty(error?.Title))
                return [error.Title];
        }
        catch
        {
            // Si no es JSON, devuelve el texto plano
            if (!string.IsNullOrEmpty(contenido))
                return [contenido];
        }

        // Fallback por código HTTP
        return response.StatusCode switch
        {
            System.Net.HttpStatusCode.Unauthorized => ["Credenciales incorrectas."],
            System.Net.HttpStatusCode.Forbidden => ["No tienes permisos para esta acción."],
            System.Net.HttpStatusCode.NotFound => ["Recurso no encontrado."],
            System.Net.HttpStatusCode.InternalServerError => ["Error interno del servidor."],
            _ => [$"Error inesperado ({(int)response.StatusCode})."]
        };
    }
}
