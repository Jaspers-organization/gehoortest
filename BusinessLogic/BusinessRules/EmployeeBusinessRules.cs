using BusinessLogic.Enums;
using BusinessLogic.Guards;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using System.Text.RegularExpressions;
using UserInterface.Stores;

namespace BusinessLogic.BusinessRules;

public class EmployeeBusinessRules
{
    public static void ValidateEmployee(Employee employee)
    {
        AssertEmployeeNumber(employee.EmployeeNumber);
        Guard.AssertValidEmail(employee.EmployeeLogin.Email);
        AssertValidInfix(employee.Infix);
        AssertValidLastName(employee.LastName);
        AssertValidFirstName(employee.FirstName);
    }
    public static void AssertAdministrator(EmployeeProjection projection, Employee employee)
    {
        if (projection.Id == employee.Id &&
         !(projection.Role == Role.Employee &&
           employee.AccountType == Role.Administrator))
            throw new Exception(ErrorMessageStore.ErrorAdministratorChanged);
    }    
    public static void AssertEmployeeNumber(string employeeNumber)
    {
        if (!IsNumeric(employeeNumber)) throw new Exception(ErrorMessageStore.ErrorEmployeeNumber);
    }
    public static void AssertValidInfix(string infix)
    {
        if (!IsValidStringMayBeNull(infix)) throw new Exception(ErrorMessageStore.ErrorInfix);
    }
    public static void AssertValidLastName(string lastName)
    {
        if (!IsValidString(lastName)) throw new Exception(ErrorMessageStore.ErrorLastName);
    }
    public static void AssertValidFirstName(string firstName)
    {
        if (!IsValidString(firstName)) throw new Exception(ErrorMessageStore.ErrorFirstName);
    }
    public static bool IsValidString(string str)
    {
        return IsLettersOnly(str) && str.Length <= 50 && str.Length >= 1;
    }
    public static bool IsValidStringMayBeNull(string str)
    {
        return (IsLettersOnly(str) && str.Length <= 50) || string.IsNullOrEmpty(str);
    }
    private static bool IsNumeric(string input)
    {
        return int.TryParse(input, out _);
    }

    private static bool IsLettersOnly(string input)
    {
        return Regex.IsMatch(input, "^[a-zA-Z]+$");
    }
}
