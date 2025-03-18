namespace Movieminds.Presentation.Responses;

public record PaginatedResponse<TData>(
    bool Success,
    string Message,
    IEnumerable<TData>? Data,
    int? Page,
    int? PageSize,
    int? TotalPages,
    int? TotalRecords
) : CollectionResponse<TData>(Success, Message, Data);
