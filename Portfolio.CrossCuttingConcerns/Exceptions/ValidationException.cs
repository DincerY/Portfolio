namespace Portfolio.CrossCuttingConcerns.Exceptions;

public class ValidationException : Exception
{
    public List<ValidationErrors> Errors { get; set; }

    public ValidationException(List<ValidationErrors> errors) : base("Validation error occurred")
    {
        Errors = errors;
    }
}