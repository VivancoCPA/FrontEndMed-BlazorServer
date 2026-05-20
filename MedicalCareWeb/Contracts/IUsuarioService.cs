using MedicalCareWeb.Common;
using MedicalCareWeb.Models.Aseguradora;
using MedicalCareWeb.Models.Usuario;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedicalCareWeb.Contracts;

public interface IUsuarioService
{
    Task<PaginatedResultDto<UsuarioDto>> GetUsuarioPaged(ListedPagedDto paginacion);
    Task<(bool exitoso, List<string> errores)> ToggleUsuarioAsync(Guid id);
    Task<(bool exitoso, List<string> errores)> Login(LoginDto request);
    Task Logout();
}