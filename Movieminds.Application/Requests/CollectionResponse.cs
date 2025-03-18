namespace Movieminds.Application.Requests;

public class CollectionResponse<TData> : Response<IEnumerable<TData>>, ICollectionResponse<TData>
{
    protected CollectionResponse(bool success, string message) : base(success, message) { }

    protected CollectionResponse(bool success, string message, IEnumerable<TData> data) : base(success, message, data) { }

    public static CollectionResponse<TData> Ok(IEnumerable<TData> data) => new(true, string.Empty, data);

    public static CollectionResponse<TData> Ok(string message, IEnumerable<TData> data) => new(true, message, data);

    public static new CollectionResponse<TData> Fail() => new(false, string.Empty);

    public static new CollectionResponse<TData> Fail(string message) => new(false, message);
}
