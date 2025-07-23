using Chat_API.DTOs.Requests.Common;
using Chat_API.DTOs.Responses.Common;

namespace Chat_API.Mappers;

public static class PagingMapper
{
    public static PagedResponse<T> ToPagedResponse<T>(this IEnumerable<T> source,
        Pagination pagination)
    {
        return new PagedResponse<T>
        {
            Items = source,
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        };
    }
}