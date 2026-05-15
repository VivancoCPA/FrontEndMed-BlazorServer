using MedicalCareWeb.Common;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Models.Aseguradora;
using MedicalCareWeb.Models.Centro;

namespace MedicalCareWeb.Services;

public class CentroService(HttpClient _http) : ICentroService
{
    public async Task<ApiResponse<bool>> CreateCentroAsync(CreateCentroRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("medical-centers", request);
        if (response.IsSuccessStatusCode)
            return new ApiResponse<bool>(true, true, string.Empty);

        var error = await Helper.LeerErrorAsync(response);
        return new ApiResponse<bool>(false, false, error);
    }

    public Task<IEnumerable<CentroDto>> GetAllCentrosAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResultDto<CentroDto>> GetCentroPaged(ListedPagedDto paginacion)
    {
        var url = $"medical-centers/paged?page={paginacion.Page}&pageSize={paginacion.PageSize}";
        if (!string.IsNullOrWhiteSpace(paginacion.Search))
            url += $"&search={Uri.EscapeDataString(paginacion.Search)}";// Asegúrate de escapar el término de búsqueda para evitar problemas con caracteres especiales

        if (!string.IsNullOrWhiteSpace(paginacion.SortBy))
            url += $"&sortBy={paginacion.SortBy}&sortDesc={paginacion.SortDesc.ToString().ToLower()}";

        var response = await _http.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<PaginatedResultDto<CentroDto>>();
            return data ?? new PaginatedResultDto<CentroDto>(
                Items: [],
                Page: paginacion.Page,
                PageSize: paginacion.PageSize,
                TotalCount: 0,
                Success: true,
                ErrorMessage: "");
        }
        //ERROR
        var error = await Helper.LeerErrorAsync(response);
        return new PaginatedResultDto<CentroDto>(
            Items: [],
            TotalCount: 0,
            Page: 0,
            PageSize: 0,
            Success: false,
            ErrorMessage: error);
    }

    public async Task<ApiResponse<bool>> ToggleAseguradoraAsync(Guid id)
    {
        var response = await _http.PatchAsync($"medical-centers/{id}/toggle-status", content: null);
        if (response.IsSuccessStatusCode)
            //var data = await response.Content.ReadFromJsonAsync<bool>();
            return new ApiResponse<bool>
            {
                Success = true,
                Data = true
            };
        // ❌ leer error backend
        var error = await Helper.LeerErrorAsync(response);
        return new ApiResponse<bool>
        {
            Success = false,
            ErrorMessage = error
        };
    }

    public async Task<ApiResponse<bool>> UpdateCentroAsync(Guid id, UpdateCentroRequestDto request)
    {
        var response = await _http.PutAsJsonAsync($"medical-centers/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            return new ApiResponse<bool>(true, true, string.Empty);
        }
        //ERROR
        var error = await Helper.LeerErrorAsync(response);
        return new ApiResponse<bool>(false, false, error);
    }
}
