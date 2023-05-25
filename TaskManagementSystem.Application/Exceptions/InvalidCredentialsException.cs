namespace TaskManagementSystem.Application.Exceptions;

public class InvalidCredentialsException: ApplicationException
{
    public List<string> Errors {get; set;}
    public InvalidCredentialsException(string message) : base(message)
    {
        Errors = new List<string>();
    }
}

