using MedicalCareWeb.Common;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Enum;
using MedicalCareWeb.Models.TipoCentro;
using static MudBlazor.CategoryTypes;
using static System.Net.WebRequestMethods;

namespace MedicalCareWeb.Services;

public class TipoCentroService(HttpClient _http) : ITipoCentroService
{
    public async Task<ApiResponse<bool>> CreateTipoCentroAsync(CreateTipoCentroRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("center-types", request);
        if (response.IsSuccessStatusCode)
            return new ApiResponse<bool>(true, true, string.Empty);

        var error = await Helper.LeerErrorAsync(response);
        return new ApiResponse<bool>(false, false, error);
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
    //
    public async Task<PaginatedResultDto<TipoCentroDto>> GetTipoCentroPaged(ListedPagedDto paginacion)
    {
        var url = $"center-types/paged?page={paginacion.Page}&pageSize={paginacion.PageSize}";
        //Console.WriteLine(url);
        if (!string.IsNullOrWhiteSpace(paginacion.Search))
            url += $"&search={Uri.EscapeDataString(paginacion.Search)}";// Asegúrate de escapar el término de búsqueda para evitar problemas con caracteres especiales

        if (!string.IsNullOrWhiteSpace(paginacion.SortBy))
            url += $"&sortBy={paginacion.SortBy}&sortDesc={paginacion.SortDesc.ToString().ToLower()}";

        var response = await _http.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<PaginatedResultDto<TipoCentroDto>>();
            return data ?? new PaginatedResultDto<TipoCentroDto>(
                Items: [],
                Page: paginacion.Page,
                PageSize: paginacion.PageSize,
                TotalCount: 0,
                Success: true,
                ErrorMessage: "");
        }
        // ❌ leer error backend
        var error = await Helper.LeerErrorAsync(response);
        return new PaginatedResultDto<TipoCentroDto>(
            Items: [],
            TotalCount: 0,
            Page: 0,
            PageSize: 0,
            Success: false,
            ErrorMessage: error
        );
    }

    public Task<ApiResponse<bool>> ToggleTipoCentroAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<bool>> UpdateTipoCentroAsync(int id, UpdateTipoCentroRequestDto request)
    {
        var response = await _http.PutAsJsonAsync($"center-types/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            return new ApiResponse<bool>(true, true, string.Empty);
        }
        //ERROR
        var error = await Helper.LeerErrorAsync(response);
        return new ApiResponse<bool>(false, false, error);
    }
}
