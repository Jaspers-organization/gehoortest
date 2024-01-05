using BusinessLogic.Interfaces.Services;
using System.Net;
using System.Net.Mail;

namespace EmailProvider;

public class EmailProvider : IEmailProvider
{
    private SmtpClient? client;
    private string? sender;

    public IEmailProvider Initialize(string host, string email, string password)
    {
        sender = email;
        client = new SmtpClient(host, 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(email, password),
        };
        return this;
    }

    public void SendEmail(string reciever, string subject, string body)
    {
        AssertInitialized();

        MailMessage email = new MailMessage(sender, reciever, subject, body)
        {
            IsBodyHtml = true
        };

        client.Send(email);
    }

    private void AssertInitialized()
    {
        if (client == null || sender == null) throw new ArgumentNullException("Email service must be initiliazed before use"); 
    }
}