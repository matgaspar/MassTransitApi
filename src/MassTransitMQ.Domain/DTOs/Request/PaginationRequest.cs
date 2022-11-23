namespace MassTransitMQ.Domain.DTOs.Request;

public class PaginationRequest
{
    public PaginationRequest()
    {
        
    }

	public PaginationRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}