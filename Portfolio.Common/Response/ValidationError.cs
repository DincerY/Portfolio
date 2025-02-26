namespace Portfolio.Common.Response;

public class ValidationError
{
    public string Property { get; set; }
    public IEnumerable<string> Errors { get; set; }
}