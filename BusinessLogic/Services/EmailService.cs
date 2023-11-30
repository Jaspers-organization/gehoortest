using BusinessLogic.Projections;
using Service.Interfaces.Services;
using System.Text.RegularExpressions;

namespace Service.Services;

public class EmailService
{
    private IEmailService emailService;

    public EmailService(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    public void SendEmail(string reciever, TestResultProjection testResult)
    {
        AssertValidEmail(reciever);

        string date = DateTime.Now.ToString("dd-MM-yyyy");
        string subject = $"Testresultaat {date}"; 
        string body = CreateEmailBody(date, testResult);

        emailService.SendEmail(reciever, subject, body);
    }

    private string CreateEmailBody(string date, TestResultProjection testResult)
    {
        return $@"
            test mail
        ";
    }

    private void AssertValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email is invalid");

        string emailPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        if (Regex.Matches(email, emailPattern).Count != 1) throw new ArgumentException("Email is invalid");
    }
}
