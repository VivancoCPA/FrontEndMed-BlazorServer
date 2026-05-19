namespace MedicalCareWeb.Models.Usuario;

public sealed class LoginDto
{
    public string email { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public bool rememberme { get; set; }=false;
}
