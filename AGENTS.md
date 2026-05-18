# MedicalCareR2 - AGENTS.md

## Scope and stack

- Single-project Blazor Web App (`InteractiveServer`) in `MedicalCareWeb/` targeting `net10.0` with MudBlazor `9.*`.
- This repo is frontend-only; API calls are made to an external backend via `HttpClient`.
- Solution file is `.slnx` (`MedicalCareR2.slnx`), not a legacy `.sln`.

## Run and verify

- Build: `dotnet build MedicalCareWeb\MedicalCareWeb.csproj`
- Run default profile (`http`, `http://localhost:5228`): `dotnet run --project MedicalCareWeb`
- Run explicit HTTPS profile (`https://localhost:7095`): `dotnet run --project MedicalCareWeb --launch-profile https`
- There are currently no tests, lint, formatter, or CI workflow files in this repo.

## Runtime wiring that is easy to break

- `Program.cs` throws on startup if `ApiSettings:BaseUrl` is missing/blank; `appsettings.Development.json` currently sets `http://localhost:5043/api/`.
- `Program.cs` registers scoped `HttpClient` with `BaseAddress = ApiSettings:BaseUrl`; service implementations depend on relative endpoint strings.
- MudBlazor providers (`MudDialogProvider`, `MudSnackbarProvider`, etc.) are defined in `Components/Layout/MainLayout.razor`; dialogs/snackbars will fail if removed.
- App shell is in `Components/App.razor` and routes use `Components/Routes.razor` with `MainLayout` as default layout.

## UI/data conventions

- `_Imports.razor` globally injects `IDialogService`, `ISnackbar`, and domain services; pages often use them without local `@inject`.
- `MudDataGrid` server paging is used across catalog pages; grid page index is 0-based but API requests convert to 1-based (`state.Page + 1`).
- Search handlers use debounce + cancellation-token patterns before reloading server data; preserve this behavior when editing list pages.
- Codebase naming is Spanish domain language (`Aseguradora`, `Centro`, `TipoCentro`, `Medico`, etc.); follow existing terms for new DTOs/services/pages.

## Service layer status

- `Services/` contains the HTTP integrations; check these first when changing API contracts.
- Not all interface methods are implemented yet: currently `AseguradoraService.GetAllAseguradoraAsync` and `CentroService.GetAllCentrosAsync` still throw `NotImplementedException`.
- Endpoints currently used include insurers, medical-centers, center-types, specialties, and doctors (including `/paged` and `/{id}/toggle-status` variants).
