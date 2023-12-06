namespace BusinessLogic.Interfaces;

public interface IHashingService
{
    public string GenerateSalt();

    public string HashPassword(string salt, string password);

    public bool VerifyPassword(string password, string hashedPassword);
}
