namespace TdpGisApi.Application.Response;

public class PagedList<T> : List<T>
{
    public PagedList(List<T> items, int pageNumber, int pageSize, string? token)
    {
        CurrentPage = pageNumber;
        PageSize = pageSize;
        ContinuationToken = token;
        AddRange(items);
    }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public string? ContinuationToken { get; set; }
}