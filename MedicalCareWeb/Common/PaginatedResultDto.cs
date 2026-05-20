
namespace MedicalCareWeb.Common;

public record PaginatedResultDto<T>
(
    IEnumerable<T> Items,
    int Page,
    int PageSize,
    int TotalCount,
    bool Success = true,
    List<string> Errores = null!,
    string? ErrorMessage = null!
    ) where T : class
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;
    }
    

