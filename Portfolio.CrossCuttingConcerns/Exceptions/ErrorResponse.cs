namespace Portfolio.CrossCuttingConcerns.Exceptions;

public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public List<ValidationErrors>? ValidationErrors { get; set; }
}