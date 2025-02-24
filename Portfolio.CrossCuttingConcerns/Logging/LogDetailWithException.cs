namespace Portfolio.CrossCuttingConcerns.Logging;

public class LogDetailWithException : ResponseLogDetail
{
    

    public string ExceptionMessage { get; set; }

    public LogDetailWithException(string user, string traceId, string httpMethod, string path, Dictionary<string, string> queryParams, int? statusCode, string userAgent, string controller, string exceptionMessage) : base(user, traceId, httpMethod, path, queryParams, statusCode, userAgent, controller)
    {
        ExceptionMessage = exceptionMessage;
    }

    public LogDetailWithException()
    {
        
    }
}