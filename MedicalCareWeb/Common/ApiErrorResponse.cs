namespace MedicalCareWeb.Common;

// Models/Common/ApiErrorResponse.cs
public class ApiErrorResponse
{
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; } = new();

    // ✅ Aplana todos los errores en una sola lista legible
    public List<string> ObtenerErrores()
    {
        return Errors.Values
            .SelectMany(e => e)
            .ToList();
    }
}