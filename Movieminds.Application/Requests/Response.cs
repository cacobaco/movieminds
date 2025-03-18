namespace Movieminds.Application.Requests;

public class Response : IResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }

    protected Response(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public static Response Ok() => new(true, string.Empty);
    public static Response Ok(string message) => new(true, message);
    public static Response Fail() => new(false, string.Empty);
    public static Response Fail(string message) => new(false, message);

    public static Response<TData> Ok<TData>() => new(true, string.Empty);
    public static Response<TData> Ok<TData>(TData data) => new(true, string.Empty, data);
    public static Response<TData> Ok<TData>(string message, TData data) => new(true, message, data);
    public static Response<TData> Fail<TData>() => new(false, string.Empty);
    public static Response<TData> Fail<TData>(string message) => new(false, message);
}

public class Response<TData> : Response, IResponse<TData>
{
    public TData? Data { get; set; }

    protected internal Response(bool success, string message) : base(success, message)
    {
    }

    protected internal Response(bool success, string message, TData data) : base(success, message)
    {
        Data = data;
    }
}
