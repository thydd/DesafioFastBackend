namespace DesafioFastBackend.API.Contracts;

public sealed record ApiErrorResponse
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
    public int StatusCode { get; init; }
    public string? TraceId { get; init; }
}
