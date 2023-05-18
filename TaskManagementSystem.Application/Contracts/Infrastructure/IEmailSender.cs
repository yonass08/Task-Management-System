using TaskManagementSystem.Application.Models.Mail;

namespace TaskManagementSystem.Application.Contracts.Identity;

public interface IEmailSender
{
    Task<Email> sendEmail(Email email);
}
