namespace TaskManagementSystem.Domain;

public class CheckList: BaseDomainEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int TaskId { get; set; }

    public Status Status { get; set; } = Status.NotStarted;

    public virtual Task Task { get; set; }
}
