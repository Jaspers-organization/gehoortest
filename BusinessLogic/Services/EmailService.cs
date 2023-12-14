using BusinessLogic.Projections;
using BusinessLogic.BusinessRules;
using BusinessLogic.Interfaces.Services;

namespace BusinessLogic.Services;

public class EmailService
{
    private IEmailProvider provider;

    public EmailService(IEmailProvider provider)
    {
        this.provider = provider;
    }

    public bool SendEmail(string reciever, Guid testResultId)
    {
        EmailBusinessRules.AssertValidEmail(reciever);

        string date = DateTime.Now.ToString("dd-MM-yyyy");
        string subject = $"Testresultaat {date}"; 
        string body = CreateEmailBody(date);

       return provider.SendEmail(reciever, subject, body);
    }

    private string CreateEmailBody(string date)
    {
        string result = true ? "Mogelijke gehoorschade" : "Gezond gehoor";

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
