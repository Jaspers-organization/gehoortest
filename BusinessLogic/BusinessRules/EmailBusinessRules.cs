using System.Text.RegularExpressions;

namespace BusinessLogic.BusinessRules;

public class EmailBusinessRules
{
    public static bool IsValidEmail(string? email)
    {
        string emailPattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        return (!string.IsNullOrEmpty(email) && Regex.Matches(email, emailPattern).Count == 1);
    }

    public static void AssertValidEmail(string? email)
    {
        if (!IsValidEmail(email)) throw new ArgumentException("Email is invalid");
    }
}

