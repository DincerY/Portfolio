namespace Portfolio.Common.Response;

public class ValidationError
{
    public string Reason { get; set; }
    public string Message { get; set; }
    public string Domain { get; set; }
}