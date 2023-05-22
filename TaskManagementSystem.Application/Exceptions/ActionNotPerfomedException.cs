namespace TaskManagementSystem.Application.Exceptions;

public class ActionNotPerfomedException : ApplicationException
{
    public List<string> Errors {get; set;}

    public ActionNotPerfomedException(string message) : base(message)
    {
        Errors = new List<string>();
    }
}