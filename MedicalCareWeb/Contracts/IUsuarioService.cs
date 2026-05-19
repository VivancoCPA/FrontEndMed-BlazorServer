using MedicalCareWeb.Common;
using MedicalCareWeb.Models.Usuario;

namespace MedicalCareWeb.Contracts;

public interface IUsuarioService
{
    Task<(bool exitoso, List<string> errores)> Login(LoginDto request);
    Task Logout();
}