using System.Text;

namespace BusinessLogic.Services;

internal class PasswordService
{
    public const char PASSWORD_SEPARATOR = '$';
    public const byte HASH_INDEX = 0;
    public const byte SALT_INDEX = 1;
    public const byte PASSWORD_INDEX = 2;

    public static string EncodeBase64(string hashedPassword)
    {
        byte[] bytes =  Encoding.UTF8.GetBytes(hashedPassword);
        return Convert.ToBase64String(bytes);
    }

    public static string DecodeBase64(string password)
    {
        byte[] bytes = Convert.FromBase64String(password);
        return Encoding.UTF8.GetString(bytes);
    }

    public static string[] DeconstructEmployeePassword(string password)
    {
        return password.Split(PASSWORD_SEPARATOR);
    }
}
