
namespace MedicalCareWeb.Common;

public record ListedPagedDto
(
int Page = 1,
int PageSize = 10,
string? Search = null,
string? SortBy = null,     
bool SortDesc = false // true = DESC, false = ASC;
    );

