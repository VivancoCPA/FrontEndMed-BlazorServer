
namespace MedicalCareWeb.Common;

public static class Helper
{
    public static async Task<string> LeerErrorAsync(HttpResponseMessage response)
    {
        try
        {
            // Intentar leer body
            var content = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(content))
                return content;

            // Fallback por status code
            return response.StatusCode switch
            {
                System.Net.HttpStatusCode.Unauthorized =>
                    "No autorizado. Debe iniciar sesión.",

                System.Net.HttpStatusCode.Forbidden =>
                    "No tiene permisos para realizar esta acción.",

                System.Net.HttpStatusCode.NotFound =>
                    "Recurso no encontrado.",

                System.Net.HttpStatusCode.BadRequest =>
                    "Solicitud inválida.",

                System.Net.HttpStatusCode.InternalServerError =>
                    "Error interno del servidor.",

                _ => response.ReasonPhrase ?? "Error inesperado"
            };
        }
        catch
        {
            return "Error inesperado";
        }
    }
}
