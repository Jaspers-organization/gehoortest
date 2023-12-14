namespace BusinessLogic.Interfaces.Services;

public interface IEmailProvider
{
    public IEmailProvider Initialize(string host, string email, string password);
    public bool SendEmail(string reciever, string subject, string body);
}
