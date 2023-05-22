using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Application.Models.Identity;

public class RegistrationModel
{
    [Required]
    [EmailAddress]
    public string Email {get; set;} = "";

    [Required]
    public string FullName {get; set;}

    [Required]
    public string UserName {get; set;} = "";

    [Required]
    public string Password{get; set;} = "";

}
