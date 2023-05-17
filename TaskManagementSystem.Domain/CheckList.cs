namespace TaskManagementSystem.Domain;

public class CheckList
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TaskId { get; set; }
    public Status Status { get; set; }

    public virtual Task Task { get; set; }
}
