namespace TaskManagementSystem.Application.Exceptions;

public class ActionNotPerfomedException : ApplicationException
{
    public ActionNotPerfomedException(string message) : base(message)
    {

    }
}