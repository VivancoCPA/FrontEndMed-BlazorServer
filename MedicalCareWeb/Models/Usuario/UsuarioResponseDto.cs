using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MedicalCareWeb.Models.Usuario
{
    public class UsuarioResponseDto
    {
        public string token {  get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        
    }
}
