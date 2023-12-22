using System.Text;
using System;
namespace BusinessLogic.Services;

public class PasswordService
{
    public const char PASSWORD_SEPARATOR = '$';
    public const byte HASH_INDEX = 0;
    public const byte SALT_INDEX = 1;
    public const byte PASSWORD_INDEX = 2;

    public static string EncodeBase64(string hashedPassword)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(hashedPassword);
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

    private static readonly List<string> Colors = new List<string>
    {
        "Blauw", "Rood", "Groen", "Geel", "Paars", "Wit", "Zwart", "Bruin"
    };

    private static readonly List<string> Names = new List<string>
    {
        "Schaap", "Hond", "Kat", "Paard", "Vis", "Boom", "Huis", "Auto", "Boot", "Vliegtuig", "Fiets", "Pop", "Bal"
    };


    public static string GeneratePassword()
    {
        Random random = new Random();
        return Colors[random.Next(0, Colors.Count)] + Names[random.Next(0, Names.Count)] + random.Next(1, 100).ToString();
    }
}