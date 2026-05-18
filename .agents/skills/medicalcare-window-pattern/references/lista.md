# Lista Reference

This reference standardizes `Lista*.razor` pages with server paging and search behavior.

## Core rules

1. Use `MudDataGrid` with server-side data loading.
2. Convert grid page index to API index with `state.Page + 1`.
3. Keep debounce + cancellation token strategy for search.
4. Preserve consistent row actions (edit, toggle status, details if needed).

## Server paging mapping

Grid is 0-based. API is 1-based.

```csharp
var pageRequest = state.Page + 1;
var pageSize = state.PageSize;
```

## Debounced search pattern

```csharp
private CancellationTokenSource? _searchCts;

private async Task OnSearchChanged(string value)
{
    _searchCts?.Cancel();
    _searchCts?.Dispose();
    _searchCts = new CancellationTokenSource();

    try
    {
        await Task.Delay(350, _searchCts.Token);
        _searchText = value;
        await _grid.ReloadServerData();
    }
    catch (OperationCanceledException)
    {
        // Expected when user keeps typing.
    }
}
```

## UX consistency

- Keep top-level page actions discoverable and stable.
- Show loading and empty states explicitly.
- Keep filters compact and aligned with domain language.
- Use snackbar for operation results (create/update/toggle/delete).

## Do and do not

Do:
- Reset page index when main filter/search changes.
- Keep API call cancellation in rapid typing scenarios.

Do not:
- Fire one HTTP request per keypress without debounce.
- Break page index mapping between grid and API.
