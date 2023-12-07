using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class Employee : IEmployee
{
    public enum role { employee, adminstrator } // in de interface TODO
    public int Id { get; set; }
    public string? EmployeeNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Infix { get; set; }
    public string? Fullname { get; set; }
}
