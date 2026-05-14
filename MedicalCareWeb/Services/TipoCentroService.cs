using MedicalCareWeb.Common;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Enum;
using MedicalCareWeb.Models.TipoCentro;
using static MudBlazor.CategoryTypes;
using static System.Net.WebRequestMethods;

namespace MedicalCareWeb.Services;

public class TipoCentroService(HttpClient _http) : ITipoCentroService
{
    public Task<ApiResponse<bool>> CreateTipoCentroAsync(CreateTipoCentroRequestDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TipoCentroDto>> GetAllTipoCentroAsync(EnumStatus status) 
    {
        var url = "center-types";

        if (status == EnumStatus.Activo)
        {
            url += "/lookup";
        }
        Console.WriteLine($"URL construida para obtener los tipos de centro: {url}");
        var response = await _http.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            // Manejar el error según sea necesario
            throw new Exception($"Error al obtener los tipos de centro: {response.ReasonPhrase}");
        }
        return await _http.GetFromJsonAsync<IEnumerable<TipoCentroDto>>(url) ?? Enumerable.Empty<TipoCentroDto>();
    }
    public Task<PaginatedResultDto<TipoCentroDto>> GetTipoCentroPaged(ListedPagedDto paginacion)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<bool>> ToggleTipoCentroAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<bool>> UpdateTipoCentroAsync(int id, UpdateTipoCentroRequestDto request)
    {
        throw new NotImplementedException();
    }
}
