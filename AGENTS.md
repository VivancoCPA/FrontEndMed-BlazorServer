# MedicalCareR2 — AGENTS.md

## Overview

Blazor Interactive Server frontend (`.NET 10`, `net10.0`) consuming an external REST API. MudBlazor v9.* UI. Single-project solution with `.slnx` format.

**No backend code lives here** — this is purely the Blazor client. The API base URL is `http://localhost:5043/api/` (configured in `appsettings.Development.json`).

## Architecture

- Entrypoint: `MedicalCareWeb/Program.cs` — registers scoped `HttpClient`, MudBlazor, and services
- Layout: `MainLayout.razor` with dark mode toggle, custom `PaletteDark`/`PaletteLight`
- Navigation: `NavMenu.razor` (Catalogos group with Centros Medicos + Aseguradoras)
- Pages use `MudDataGrid` with `ServerData` for server-side pagination
- CRUD via MudBlazor dialogs (`DialogService.ShowAsync`)

## Directory Layout

| Path | Purpose |
|------|---------|
| `Models/{Domain}/` | DTOs per domain: `Dto`, `Create*RequestDto`, `Update*RequestDto` |
| `Contracts/` | Service interfaces |
| `Services/` | HTTP service implementations (many methods still `NotImplementedException`) |
| `Common/` | Shared DTOs (`ApiResponse<T>`, `PaginatedResultDto<T>`, `ListedPagedDto`), file storage, helpers |
| `Components/Pages/{Domain}/` | Razor pages with `MudDataGrid` + dialog-based CRUD |
| `Shared/` | Reusable components: `CardKpi.razor`, `InputImg.razor` (logo upload) |
| `Auth/`, `Infrastructure/` | Empty folders — not yet wired |

## API Endpoints (hardcoded in Services)

| Service | Endpoints |
|---------|-----------|
| Aseguradora | `insurers`, `insurers/paged`, `insurers/{id}`, `insurers/{id}/toggle-status` |
| Centro | `medical-centers/paged`, `medical-centers/{id}/toggle-status` |
| TipoCentro | `center-types`, `center-types/lookup` |

## Important Conventions & Gotchas

- `_Imports.razor` globally injects `ISnackbar`, `IDialogService`, and all service interfaces — every page has them without explicit `@inject`
- **MudDataGrid is 0-based** for pages, but the API expects **1-based** — `ListaAseguradoras.razor:414` adds `+ 1`
- Search uses 400ms debounce with `CancellationTokenSource` cancel pattern
- File upload: `InputImg.razor` converts `IBrowserFile` → `ArchivoDTO` via `ConvertirAArchivoDTO()`, then `AlmacenadorArchivosLocal` saves to `wwwroot/{contenedor}/`
- `ApiSettings:BaseUrl` is **required** in config (throws `InvalidOperationException` if missing)
- Solution uses `.slnx` format (not legacy `.sln`) — open with VS 2022+
- `<Nullable>enable</Nullable>` and `<ImplicitUsings>enable</ImplicitUsings>`
- `BlazorDisableThrowNavigationException` is set to `true` to suppress Blazor navigation errors

## Developer Commands

```powershell
# Build
dotnet build MedicalCareWeb\MedicalCareWeb.csproj

# Run (development)
dotnet run --project MedicalCareWeb

# Run with explicit profile
dotnet run --project MedicalCareWeb --launch-profile https
```

No tests, CI, linters, formatters, or typecheckers are configured.

## Naming & Style

- Code is in Spanish (domain terms: Aseguradora, Centro, TipoCentro, Archivo, Almacenador)
- Service classes use primary constructor injection: `public class FooService(HttpClient _http) : IFooService`
- `ApiResponse<T>` supports both constructor (`ApiResponse<T>(data, success, error)`) and object initializer
- `PaginatedResultDto<T>` includes computed properties `TotalPages`, `HasPreviousPage`, `HasNextPage`
