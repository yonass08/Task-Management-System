namespace TaskManagementSystem.Domain;

public abstract class BaseDomainEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime LastUpdated { get; set; }

}
