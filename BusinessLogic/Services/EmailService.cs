using BusinessLogic.Projections;
using BusinessLogic.BusinessRules;
using BusinessLogic.Interfaces.Services;

namespace BusinessLogic.Services;

public class EmailService
{
    private IEmailService emailService;

    public EmailService(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    public void SendEmail(string reciever, TestResultProjection testResult)
    {
        EmailBusinessRules.AssertValidEmail(reciever);

        string date = DateTime.Now.ToString("dd-MM-yyyy");
        string subject = $"Testresultaat {date}"; 
        string body = CreateEmailBody(date, testResult);

        emailService.SendEmail(reciever, subject, body);
    }

    private string CreateEmailBody(string date, TestResultProjection testResult)
    {
        string result = testResult.hasHearingLoss ? "Mogelijke gehoorschade" : "Gezond gehoor";

        return $@"
            <html>
                <body>
                    <h1 style='text-align:center'>Resultaten gehoortest</h1>
                    <h2 style='text-align:center;color:#DA0063'>{result}</h2>
                    <br>
                    <span>U heeft een gehoortest bij ons gedaan op:</span>
                    <span style='display:block'>{date}</span>
                    <br>
                    <span style='display:block'>De resultaten duiden op <span style='color:#DA0063'>{result.ToLower()}</span>.</span>
                    <br>
                    <span style='display:block'>U gaf aan de getestte frequenties op onderstaand decibel niveau te horen:</span>
                    <br>
                    
                    <br>
                    <span style='display:block'>De audicien kan u advies geven op basis van de testresultaten. maak een afspraak met de audicien.</span>
                </body>
            </html>         
        ";
    }
}
