namespace BusinessLogic.Interfaces;

public interface IHashingService
{
    public string GenerateSalt();

    public string HashPassword(string password, string salt);

    public bool VerifyPassword(string password, string hashedPassword);
}
