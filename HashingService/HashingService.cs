using BusinessLogic.Interfaces;

namespace HashingService;

public class HashingService : IHashingService
{
    public string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }

    public string HashPassword(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool VerifyPassword(string password, string salt, string passwordToVerify)
    {
        string passwordHash = HashPassword(password, salt);

        return passwordToVerify.Equals(passwordHash);
    }
}
