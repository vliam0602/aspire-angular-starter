namespace UserManagement.Features.UserManagement.DTOs;

public class PaginatedResult<T>
{
    public List<T> Items { get;}
    public int TotalCount { get;}
    public int PageIndex { get;}
    public int PageSize { get;}

    public PaginatedResult(List<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}
