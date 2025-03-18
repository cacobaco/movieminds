namespace Movieminds.Application.Requests;

public interface IResponse
{
    bool Success { get; set; }
    string Message { get; set; }
}

public interface IResponse<TData> : IResponse
{
    TData? Data { get; set; }
}
