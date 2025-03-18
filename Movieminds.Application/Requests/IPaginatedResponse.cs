namespace Movieminds.Application.Requests;

public interface IPaginatedResponse<TData> : ICollectionResponse<TData>
{
    int? Page { get; }
    int? PageSize { get; }
    int? TotalPages { get; }
    int? TotalRecords { get; }
}
