namespace TaskManagementSystem.Domain;

public class UserTask: BaseDomainEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int UserId { get; set; }

    public Status Status { get; set; } = Status.NotStarted;

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<CheckList> CheckLists { get; set; }
}