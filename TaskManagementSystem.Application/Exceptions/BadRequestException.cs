namespace TaskManagementSystem.Application.Exceptions;

public class BadRequestException : ApplicationException
{
    public List<string> Errors {get; set;}
    public BadRequestException(string message) : base(message)
    {
        Errors = new List<string>();
    }
}
