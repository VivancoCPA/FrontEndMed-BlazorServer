
using MedicalCareWeb.Common;
using MedicalCareWeb.Models.Aseguradora;

namespace MedicalCarweb.Contracts;

public interface IAseguradoraService
{
    Task<IEnumerable<AseguradoraDto>> GetAllAseguradoraAsync();
    Task<PaginatedResultDto<AseguradoraDto>> GetAseguradoraPaged(ListedPagedDto paginacion);
    Task<ApiResponse<bool>> CreateAseguradoraAsync(CreateAseguradoraRequestDto request);
    Task<ApiResponse<bool>> UpdateAseguradoraAsync(Guid id, UpdateAseguradoraRequestDto request);
    Task<ApiResponse<bool>> ToggleAseguradoraAsync(Guid id);

}
