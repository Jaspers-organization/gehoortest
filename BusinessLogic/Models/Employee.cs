using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class Employee : IModel
{
    public Guid Id { get; set; }
    public enum Role { Employee, Administrator } // Enum names are conventionally PascalCase

    public string? EmployeeNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Infix { get; set; }

    public string? FullName
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            {
                if (!string.IsNullOrWhiteSpace(Infix))
                {
                    return $"{FirstName} {Infix} {LastName}";
                }
                else
                {
                    return $"{FirstName} {LastName}";
                }
            }
            return null;
        }
    }

    public ICollection<Test>? Tests { get; set; }
}

