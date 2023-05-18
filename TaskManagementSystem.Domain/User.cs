namespace TaskManagementSystem.Domain;

public class User: BaseDomainEntity
{
    public int Id { get; set; }

    public string FullName { get; set; }
    
    public string Email { get; set; }
}
