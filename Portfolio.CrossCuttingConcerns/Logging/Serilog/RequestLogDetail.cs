namespace Portfolio.CrossCuttingConcerns.Logging.Serilog;

public class RequestLogDetail
{
    public string TraceId { get; set; }  
    public string HttpMethod { get; set; }
    public string ContentType { get; set; }
    public string Path { get; set; }
}