using MedicalCareWeb.Common;
using MedicalCareWeb.Models.Centro;


namespace MedicalCareWeb.Contracts;

public interface ICentroService
{
    Task<IEnumerable<CentroDto>> GetAllCentrosAsync();
    Task<PaginatedResultDto<CentroDto>> GetCentroPaged(ListedPagedDto paginacion);
    Task<ApiResponse<bool>> CreateCentroAsync(CreateCentroRequestDto request);
    Task<ApiResponse<bool>> UpdateCentroAsync(Guid id, UpdateCentroRequestDto request);
    Task<ApiResponse<bool>> ToggleAseguradoraAsync(Guid id);
}
