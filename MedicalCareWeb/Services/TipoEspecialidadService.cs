using MedicalCareWeb.Common;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Enum;
using MedicalCareWeb.Models.TipoEspecialidad;

namespace MedicalCareWeb.Services;

public class TipoEspecialidadService(HttpClient _http) : ITipoEspecialidadService
{
    public async Task<ApiResponse<bool>> CreateTipoEspecialidadAsync(CreateTipoEspecialidadRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("specialties", request);
        if (response.IsSuccessStatusCode)
            return new ApiResponse<bool>(true, true, string.Empty);

        var error = await Helper.LeerErrorAsync(response);
        return new ApiResponse<bool>(false, false, error);
    }

    public async Task<IEnumerable<TipoEspecialidadDto>> GetAllTipoEspecialidadAsync(EnumStatus status)
    {
        var url = "specialties";

        if (status == EnumStatus.Activo)
        {
            url += "/lookup";
        }
        Console.WriteLine($"URL construida para obtener los tipos de centro: {url}");
        var response = await _http.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            // Manejar el error según sea necesario
            throw new Exception($"Error al obtener los tipos de Especialidad: {response.ReasonPhrase}");
        }
        return await _http.GetFromJsonAsync<IEnumerable<TipoEspecialidadDto>>(url) ?? Enumerable.Empty<TipoEspecialidadDto>();
    }

    public async Task<PaginatedResultDto<TipoEspecialidadDto>> GetTipoEspecialidadPaged(ListedPagedDto paginacion)
    {
        var url = $"specialties/paged?page={paginacion.Page}&pageSize={paginacion.PageSize}";
        //Console.WriteLine(url);
        if (!string.IsNullOrWhiteSpace(paginacion.Search))
            url += $"&search={Uri.EscapeDataString(paginacion.Search)}";// Asegúrate de escapar el término de búsqueda para evitar problemas con caracteres especiales

        if (!string.IsNullOrWhiteSpace(paginacion.SortBy))
            url += $"&sortBy={paginacion.SortBy}&sortDesc={paginacion.SortDesc.ToString().ToLower()}";

        var response = await _http.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<PaginatedResultDto<TipoEspecialidadDto>>();
            return data ?? new PaginatedResultDto<TipoEspecialidadDto>(
                Items: [],
                Page: paginacion.Page,
                PageSize: paginacion.PageSize,
                TotalCount: 0,
                Success: true,
                ErrorMessage: "");
        }
        // ❌ leer error backend
        var error = await Helper.LeerErrorAsync(response);
        return new PaginatedResultDto<TipoEspecialidadDto>(
            Items: [],
            TotalCount: 0,
            Page: 0,
            PageSize: 0,
            Success: false,
            ErrorMessage: error
        );
    }

    public async Task<ApiResponse<bool>> UpdateTipoEspecialidadAsync(int id, UpdateTipoEspecialidadRequestDto request)
    {
        var response = await _http.PutAsJsonAsync($"specialties/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            return new ApiResponse<bool>(true, true, string.Empty);
        }
        //ERROR
        var error = await Helper.LeerErrorAsync(response);
        return new ApiResponse<bool>(false, false, error);
    }
}
