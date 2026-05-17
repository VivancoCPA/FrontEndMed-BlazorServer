using MedicalCareWeb.Common;
using MedicalCareWeb.Enum;
using MedicalCareWeb.Models.TipoEspecialidad;

namespace MedicalCareWeb.Contracts;

public interface ITipoEspecialidadService
{
    Task<IEnumerable<TipoEspecialidadDto>> GetAllTipoEspecialidadAsync(EnumStatus status);
    Task<PaginatedResultDto<TipoEspecialidadDto>> GetTipoEspecialidadPaged(ListedPagedDto paginacion);
    Task<ApiResponse<bool>> CreateTipoEspecialidadAsync(CreateTipoEspecialidadRequestDto request);
    Task<ApiResponse<bool>> UpdateTipoEspecialidadAsync(int id, UpdateTipoEspecialidadRequestDto request);
    
}
