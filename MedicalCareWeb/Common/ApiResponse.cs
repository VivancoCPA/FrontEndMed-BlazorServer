namespace MedicalCareWeb.Common;
public class ApiResponse<T>
{
    // Constructor vacío — permite el uso con object initializer: new ApiResponse<T> { ... }
    public ApiResponse() { }

    // Constructor posicional — new ApiResponse<T>(data, success, errorMessage)
    public ApiResponse(T? data, bool success, string? errorMessage)
    {
        Data = data;
        Success = success;
        ErrorMessage = errorMessage;
    }

    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    public Dictionary<string, string[]>? Errors { get; set; } = null; // Para errores de validación (400)
}
