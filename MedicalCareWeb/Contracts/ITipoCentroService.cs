using MedicalCareWeb.Common;
using MedicalCareWeb.Enum;
using MedicalCareWeb.Models.TipoCentro;

namespace MedicalCareWeb.Contracts;

public interface ITipoCentroService
{
    Task<IEnumerable<TipoCentroDto>> GetAllTipoCentroAsync(EnumStatus status);
    Task<PaginatedResultDto<TipoCentroDto>> GetTipoCentroPaged(ListedPagedDto paginacion);
    Task<ApiResponse<bool>> CreateTipoCentroAsync(CreateTipoCentroRequestDto request);
    Task<ApiResponse<bool>> UpdateTipoCentroAsync(int id, UpdateTipoCentroRequestDto request);
    Task<ApiResponse<bool>> ToggleTipoCentroAsync(int id);
}
