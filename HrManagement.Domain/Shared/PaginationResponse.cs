namespace HrManagement.Domain.Shared;

public class PaginationResponse<T> where T : class
{
    public T Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int totalCount { get; set; }
    public bool IsFirstPage => PageNumber == 1;
    public bool IsLastPage => PageNumber >= (int)Math.Ceiling((double)totalCount / PageSize);
}
