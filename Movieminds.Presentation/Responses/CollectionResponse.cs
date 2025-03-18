namespace Movieminds.Presentation.Responses;

public record CollectionResponse<TData>(bool Success, string Message, IEnumerable<TData>? Data) : Response<IEnumerable<TData>>(Success, Message, Data);
