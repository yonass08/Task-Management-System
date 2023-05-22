using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Identity.Models;

public class TaskManagementSystemUser : IdentityUser
{
    public string FullName {get; set;}
}
