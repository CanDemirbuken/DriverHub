namespace DriverHub.Application.Common.Models;

public sealed record PagedResponse<T> where T : class
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;


    public static PagedResponse<T> CreateResponse(IReadOnlyList<T> items, int pageNumber, int pageSize, int totalCount)
    {
        return new PagedResponse<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}