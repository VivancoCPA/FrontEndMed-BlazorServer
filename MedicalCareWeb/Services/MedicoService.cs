using MedicalCareWeb.Common;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Models.Aseguradora;
using MedicalCareWeb.Models.Centro;
using MedicalCareWeb.Models.Medico;

namespace MedicalCareWeb.Services;

public class MedicoService(HttpClient _http, IAlmacenadorArchivos almacenadorArchivos) : IMedicoService
{
    private readonly string contenedor = "avatar_medico";

    public async Task<ApiResponse<bool>> CreateMedicoAsync(CreateMedicoRequestDto request)
    {
        if (request.Archivo is not null)
        {
            var url = await almacenadorArchivos.Almacenar(contenedor, request.Archivo);
            request.photoUrl = url;
        }

        var response = await _http.PostAsJsonAsync("doctors", request);
        if (response.IsSuccessStatusCode)
            return new ApiResponse<bool>(true, true, string.Empty);

        var error = await Helper.LeerErrorAsync(response);
        //borramos el archivo guardado en contenedor si hubo error
        if (request.Archivo is not null)
        {
            await almacenadorArchivos.Borrar(request.photoUrl, contenedor);
        }
        return new ApiResponse<bool>(false, false, error);
    }
    //
    public async Task<ApiResponse<bool>> UpdateMedicoAsync(Guid id, UpdateMedicoRequestDto request)
    {
        var oldUrl = request.photoUrl;

        if (request.Archivo is not null)
        {
            var url = await almacenadorArchivos.Almacenar(contenedor, request.Archivo);
            request.photoUrl = url;
        }
        //
        var response = await _http.PutAsJsonAsync($"doctors/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            //borramos el archivo anterior
            if (oldUrl is not null && request.photoUrl != oldUrl)
            {
                await almacenadorArchivos.Borrar(oldUrl, contenedor);
            }
            return new ApiResponse<bool>(true, true, string.Empty);
        }
        //ERROR
        var error = await Helper.LeerErrorAsync(response);
        //borramos el archivo guardado en contenedor si hubo error
        if (request.Archivo is not null)
        {
            await almacenadorArchivos.Borrar(request.photoUrl, contenedor);
        }

        return new ApiResponse<bool>(false, false, error);

    }
    //
    public async Task<PaginatedResultDto<MedicoDto>> GetMedicoPaged(ListedPagedDto paginacion)
    {
        var url = $"doctors/paged?page={paginacion.Page}&pageSize={paginacion.PageSize}";

        if (!string.IsNullOrWhiteSpace(paginacion.Search))
            url += $"&search={Uri.EscapeDataString(paginacion.Search)}";

        if (!string.IsNullOrWhiteSpace(paginacion.SortBy))
            url += $"&sortBy={paginacion.SortBy}&sortDesc={paginacion.SortDesc.ToString().ToLower()}";

        //Console.WriteLine(url);
        var response = await _http.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<PaginatedResultDto<MedicoDto>>();
            return data ?? new PaginatedResultDto<MedicoDto>(
                Items: [],
                Page: paginacion.Page,
                PageSize: paginacion.PageSize,
                TotalCount: 0,
                Success: true,
                ErrorMessage: "");
        }

        var error = await Helper.LeerErrorAsync(response);
        return new PaginatedResultDto<MedicoDto>(
            Items: [],
            TotalCount: 0,
            Page: 0,
            PageSize: 0,
            Success: false,
            ErrorMessage: error);
    }

    public async Task<ApiResponse<bool>> ToggleMedicoAsync(Guid id)
    {
        var response = await _http.PatchAsync($"doctors/{id}/toggle-status", content: null);
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
}
