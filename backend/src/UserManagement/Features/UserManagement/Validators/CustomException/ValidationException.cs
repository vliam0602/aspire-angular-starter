namespace UserManagement.Features.UserManagement.Validators.CustomException;

public class CustomValidationException : Exception
{
    public List<ValidationError> Errors { get; }

    public CustomValidationException(List<ValidationError> errors)
        : base("Validation failed")
    {
        Errors = errors;
    }

    public override string ToString()
    {
        var errorMessages = Errors.Select(e => e.ToString());
        return $"{Message}\n{string.Join("\n", errorMessages)}";
    }
}
