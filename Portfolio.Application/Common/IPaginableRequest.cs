namespace Portfolio.Application.Common;

public interface IPaginableRequest
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
}