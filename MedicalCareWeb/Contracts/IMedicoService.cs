using MedicalCareWeb.Common;
using MedicalCareWeb.Models.Medico;

namespace MedicalCareWeb.Contracts;

public interface IMedicoService
{
    Task<PaginatedResultDto<MedicoDto>> GetMedicoPaged(ListedPagedDto paginacion);
    Task<ApiResponse<bool>> CreateMedicoAsync(CreateMedicoRequestDto request);
    Task<ApiResponse<bool>> UpdateMedicoAsync(Guid id, UpdateMedicoRequestDto request);
    Task<ApiResponse<bool>> ToggleMedicoAsync(Guid id);
}
