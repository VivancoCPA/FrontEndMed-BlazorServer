---
name: medicalcare-window-pattern
description: Use when creating or refactoring MedicalCareWeb windows (DataEntry* and Lista*) to keep the existing MudBlazor UI pattern, layout, naming, and behavior conventions.
---

# MedicalCare Window Pattern

Apply this skill for new windows in `MedicalCareWeb/Components/Pages/**`.

## Scope

Use for:
- DataEntry screens and dialog content (`DataEntry*.razor` + optional `.razor.css`).
- List screens (`Lista*.razor`) with filters, server paging, and row actions.

Do not use for:
- App startup wiring (`Program.cs`, providers, `App.razor`, `Routes.razor`).
- Backend/API implementation (this repo is frontend-only).

## Project Anchors (must respect)

- UI library: MudBlazor 9.x.
- Domain language: Spanish naming in UI/domain terms (`Aseguradora`, `Centro`, `TipoCentro`, `Medico`, etc.).
- Notifications: use `Snackbar` for success/warning/error feedback.
- Existing app dependencies are globally available in `_Imports.razor`; do not add duplicate injects unless needed.

## DataEntry Pattern (required defaults)

Follow the pattern already used in:
- `MedicalCareWeb/Components/Pages/Aseguradoras/DataEntryAseguradora.razor`
- `MedicalCareWeb/Components/Pages/Centros/DataEntryCentro.razor`

### Structure

1. Root `MudForm` with `@ref` and model binding.
2. Visual wrapper container: `div.dea-shell`.
3. Form sections with small uppercase labels using `div.dea-section-label` + icon.
4. Inputs grouped in `MudGrid` / `MudItem` responsive layout.
5. Status control block using `dea-status-pill` and `MudSwitch`.
6. Action footer using `div.dea-actions` with cancel + submit buttons.

### Input conventions

- Use `Variant="Variant.Outlined"`, `Margin="Margin.Dense"`, and `FullWidth="true"` in standard fields.
- Set `Required="true"` and `RequiredError` for mandatory fields.
- Keep labels and validation texts in Spanish.
- Keep icon adornments for key fields when consistent with nearby forms.

### Submit conventions

- Validate with `await _form!.ValidateAsync();` before submit.
- If invalid, show warning `Snackbar` and return.
- Split create/edit logic (`NewSubmit` / `EditSubmit`) when both modes exist.
- Close modal with `MudDialog?.Close(DialogResult.Ok(true));` on success.

### Style conventions

- Reuse existing CSS class family: `dea-*`.
- Keep visual tone subtle and consistent with Mud palette variables.
- Avoid introducing a new visual system if existing `dea-*` is sufficient.

See detailed tokens and examples in `references/dataentry.md`.

## Lista Pattern (required defaults)

For list pages, preserve server-driven grid behavior.

### Data loading

- Use `MudDataGrid` server data mode.
- Keep page index conversion: grid is 0-based, API request is 1-based (`state.Page + 1`).
- Respect current service contracts and relative endpoints.

### Search/filter behavior

- Use debounce + `CancellationTokenSource` pattern before reloading server data.
- Cancel previous pending search requests when user types again.
- Reset pagination to first page when changing main filters/search.

### UX behavior

- Keep primary actions visible (create/new, edit, toggle state).
- Show empty/loading/error states clearly using MudBlazor components.
- Keep actions and labels in Spanish domain language.

See checklist and implementation notes in `references/lista.md`.

## Reuse-first rules

- Prefer extracting repeated CSS/UI fragments before creating new variants.
- Keep class names and section order aligned with existing windows unless there is a clear UX reason.
- Keep code-behind naming consistent (`Load`, `Submit`, `Cancel`, `On...Changed`, etc.).

## Definition of Done (UI)

Before finishing a window:
1. Layout and sections match the existing product pattern.
2. Validation and snackbar feedback are complete.
3. Responsive behavior works for mobile and desktop breakpoints.
4. List pages keep debounce + cancellation + server paging mapping.
5. Naming and wording remain in Spanish domain style.

Use `references/checklist.md` as final gate.
