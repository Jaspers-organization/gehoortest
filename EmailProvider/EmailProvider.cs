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

    public bool SendEmail(string reciever, string subject, string body)
    {
        AssertInitialized();

        MailMessage email = new MailMessage(sender, reciever, subject, body)
        {
            IsBodyHtml = true
        };

        try
        {
            client.Send(email);
            return true;

        }
        catch(Exception ex)
        {
            //todo
            return false;
        }


    }

    //public async Task<bool> SendEmail(string receiver, string subject, string body)
    //{
    //    AssertInitialized();

    //    try
    //    {
    //        MailMessage email = new MailMessage(sender, receiver, subject, body)
    //        {
    //            IsBodyHtml = true
    //        };

    //        await client.SendMailAsync(email);
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {

    //        Console.WriteLine($"Failed to send email: {ex.Message}");
    //        return false;
    //    }
    //}


    private void AssertInitialized()
    {
        if (client == null || sender == null) throw new ArgumentNullException("Email service must be initiliazed before use"); 
    }
}