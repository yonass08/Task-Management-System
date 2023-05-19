using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Models.Mail;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace TaskManagementSystem.Infrastructure.Mail;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    private MimeMessage CreateEmailMessage(Email email)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailSettings.From));
        emailMessage.To.Add(new MailboxAddress("email", email.To));
        emailMessage.Subject = email.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = email.Body };
        return emailMessage;
    }

    public async Task sendEmail(Email email)
    {
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            var sent = await client.SendAsync(CreateEmailMessage(email));
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }

        return;
    }
}
