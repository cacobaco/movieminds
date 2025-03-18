namespace Movieminds.Application.Requests;

public class PaginatedResponse<TData> : CollectionResponse<TData>, IPaginatedResponse<TData>
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public int? TotalPages { get; set; }
    public int? TotalRecords { get; set; }

    protected PaginatedResponse(bool success, string message) : base(success, message) { }

    protected PaginatedResponse(bool success, string message, IEnumerable<TData> data) : base(success, message, data) { }

    protected PaginatedResponse(bool success, string message, IEnumerable<TData> data, int page, int pageSize) : base(success, message, data)
    {
        Page = page;
        PageSize = pageSize;
    }

    protected PaginatedResponse(bool success, string message, IEnumerable<TData> data, int page, int pageSize, int totalRecords) : base(success, message, data)
    {
        Page = page;
        PageSize = pageSize;
        TotalPages = totalRecords > 0 ? (int)Math.Ceiling(totalRecords / (double)pageSize) : 0;
        TotalRecords = totalRecords;
    }

    protected PaginatedResponse(bool success, string message, IEnumerable<TData> data, int page, int pageSize, int totalPages, int totalRecords) : base(success, message, data)
    {
        Page = page;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalRecords = totalRecords;
    }

    public static PaginatedResponse<TData> Ok(IEnumerable<TData> data, int page, int pageSize) => new(true, string.Empty, data, page, pageSize);

    public static PaginatedResponse<TData> Ok(IEnumerable<TData> data, int page, int pageSize, int totalRecords) => new(true, string.Empty, data, page, pageSize, totalRecords);

    public static PaginatedResponse<TData> Ok(IEnumerable<TData> data, int page, int pageSize, int totalPages, int totalRecords) => new(true, string.Empty, data, page, pageSize, totalPages, totalRecords);

    public static PaginatedResponse<TData> Ok(string message, IEnumerable<TData> data, int page, int pageSize, int totalRecords) => new(true, message, data, page, pageSize, totalRecords);

    public static PaginatedResponse<TData> Ok(string message, IEnumerable<TData> data, int page, int pageSize, int totalPages, int totalRecords) => new(true, message, data, page, pageSize, totalPages, totalRecords);

    public static new PaginatedResponse<TData> Fail() => new(false, string.Empty);

    public static new PaginatedResponse<TData> Fail(string message) => new(false, message);
}
