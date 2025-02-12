namespace Portfolio.CrossCuttingConcerns.Exceptions;

public class ValidationErrors
{
    public string Reason { get; set; }
    public string Message { get; set; }
    public string Domain { get; set; }
}