using Portfolio.Common.Response;

namespace Portfolio.CrossCuttingConcerns.Exceptions;

public class ValidationException : Exception
{
    public List<ValidationError> Errors { get; set; }

    public ValidationException(string message) : base(message)
    {
        
    }

    public ValidationException(List<ValidationError> errors) : base("Validation error occurred")
    {
        Errors = errors;
    }

}