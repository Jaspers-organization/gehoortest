namespace Tests;

public class HashingServiceTest
{
    [Fact]
    public void It_generates_a_salt()
    {
        HashingService.HashingService service = new HashingService.HashingService();

        string salt = service.GenerateSalt();

        Assert.Equal(29, salt.Length);
    }

    [Fact]
    public void It_hashes_a_password()
    {
        HashingService.HashingService service = new HashingService.HashingService();

        string password = "VerySecurePassword";
        string salt = service.GenerateSalt();

        string passwordHash = service.HashPassword(password, salt);

        Assert.Equal(60, passwordHash.Length);
    }

    [Theory]
    [InlineData("CorrectPassword", true)]
    [InlineData("IncorrectPassword", false)]
    public void It_verifies_a_password(string password, bool isCorrect)
    {
        HashingService.HashingService service = new HashingService.HashingService();
        
        string salt = service.GenerateSalt();
        string realPassword = service.HashPassword("CorrectPassword", salt);

        bool result = service.VerifyPassword(password, salt, realPassword);

        Assert.Equal(isCorrect, result);
    }
}
