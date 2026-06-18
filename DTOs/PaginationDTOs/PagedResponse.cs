namespace TraineeManagement.DTOs;
public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }

    public PagedResponse(IEnumerable<T> data, int count, int pageNumber, int pageSize)
    {
        Data = data;
        TotalRecords = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
