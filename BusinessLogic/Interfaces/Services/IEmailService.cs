namespace Service.Interfaces.Services;

public interface IEmailService
{
    public IEmailService Initialize(string host, string email, string password);
    public void SendEmail(string reciever, string subject, string body);
}
