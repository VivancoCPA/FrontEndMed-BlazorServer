# Window UI Checklist

Use this checklist before considering a new window done.

## General

- Uses MudBlazor components and existing project conventions.
- Uses Spanish domain naming for labels, actions, and messages.
- Keeps responsive layout for mobile (`xs`) and desktop (`sm/md`).

## DataEntry

- Uses `MudForm` validation before submit.
- Mandatory fields include `Required` and `RequiredError`.
- Shows warning snackbar when invalid.
- Shows success/error snackbar after API operations.
- Keeps `dea-*` style pattern (`dea-shell`, section labels, actions, state).

## Lista

- Uses server data loading pattern.
- Converts grid page index to API index (`state.Page + 1`).
- Search uses debounce + cancellation token.
- Filter/search changes reset to first page.
- Includes clear empty/loading/error states.

## Final review

- No duplicated injects already covered by `_Imports.razor`.
- No visual/style drift from existing windows without explicit reason.
- No hardcoded API absolute URLs in page components.
