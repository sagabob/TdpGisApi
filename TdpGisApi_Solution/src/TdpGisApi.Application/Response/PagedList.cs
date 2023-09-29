namespace TdpGisApi.Application.Response;

public class PagedList<T>
{
    public PagedList(List<T> items, int pageNumber, int pageSize, string? token )
    {
        CurrentPage = pageNumber;
        PageSize = pageSize;
        ContinuationToken = token;
        Items = items;
    }

    public List<T> Items { get; private set; }
    public int CurrentPage { get; private set; }

    public int PageSize { get; private set; }

    public string? ContinuationToken { get; set; }

}