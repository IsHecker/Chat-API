namespace Chat_API.DTOs.Requests.Common;

public class Pagination
{
    public static Pagination Default { get; } = new();

    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 5;
    public int PageNumber { get; init; } = DefaultPageNumber;
    public int PageSize { get; init; } = DefaultPageSize;
}