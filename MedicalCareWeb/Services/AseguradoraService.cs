using MedicalCareWeb.Common;
using MedicalCareWeb.Contracts;
using MedicalCareWeb.Models.Aseguradora;

namespace MedicalCareWeb.Services;

public class  AseguradoraService(HttpClient _http , IAlmacenadorArchivos almacenadorArchivos) : IAseguradoraService
{
    private readonly string contenedor = "logo_aseguradoras";

    public async Task<ApiResponse<bool>> CreateAseguradoraAsync(CreateAseguradoraRequestDto request)
    {
        if (request.Archivo is not null)
        {
            var url = await almacenadorArchivos.Almacenar(contenedor, request.Archivo);
            request.LogoUrl = url;
        }

        var response = await _http.PostAsJsonAsync("insurers", request);
        if (response.IsSuccessStatusCode)
            return new ApiResponse<bool>(true, true, string.Empty);

        var error = await Helper.LeerErrorAsync(response);
        //borramos el archivo guardado en contenedor si hubo error
        if (request.Archivo is not null)
        {
            await almacenadorArchivos.Borrar(request.LogoUrl, contenedor);
        }
        return new ApiResponse<bool>(false, false, error);
    }
    //
    public async Task<ApiResponse<bool>> UpdateAseguradoraAsync(Guid id, UpdateAseguradoraRequestDto request)
    {
        var oldUrl = request.LogoUrl;

        if (request.Archivo is not null)
        {
            var url = await almacenadorArchivos.Almacenar(contenedor, request.Archivo);
            request.LogoUrl = url;
        }
        //
        var response = await _http.PutAsJsonAsync($"insurers/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            //borramos el archivo anterior
            if (oldUrl is not null && request.LogoUrl != oldUrl)
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
            await almacenadorArchivos.Borrar(request.LogoUrl, contenedor);
        }
       
        return new ApiResponse<bool>(false, false, error);

    }
    //
    public Task<IEnumerable<AseguradoraDto>> GetAllAseguradoraAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResultDto<AseguradoraDto>> GetAseguradoraPaged(ListedPagedDto paginacion)
    {
        var url = $"insurers/paged?page={paginacion.Page}&pageSize={paginacion.PageSize}";

        if (!string.IsNullOrWhiteSpace(paginacion.Search))
            url += $"&search={Uri.EscapeDataString(paginacion.Search)}";

        if (!string.IsNullOrWhiteSpace(paginacion.SortBy))
            url += $"&sortBy={paginacion.SortBy}&sortDesc={paginacion.SortDesc.ToString().ToLower()}";

        //Console.WriteLine(url);
        var response = await _http.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<PaginatedResultDto<AseguradoraDto>>();
            return data ?? new PaginatedResultDto<AseguradoraDto>(
                Items: [],
                Page: paginacion.Page,
                PageSize: paginacion.PageSize,
                TotalCount: 0,
                Success: true,
                ErrorMessage: "");
        }

        var error = await Helper.LeerErrorAsync(response);
        return new PaginatedResultDto<AseguradoraDto>(
            Items: [],
            TotalCount: 0,
            Page: 0,
            PageSize: 0,
            Success: false,
            ErrorMessage: error);
    }

    public async Task<ApiResponse<bool>> ToggleAseguradoraAsync(Guid id)
    {
        var response = await _http.PatchAsync($"insurers/{id}/toggle-status", content: null);
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
