namespace Movieminds.Presentation.Responses;

public record Response(bool Success, string Message);

public record Response<T>(bool Success, string Message, T? Data) : Response(Success, Message);
