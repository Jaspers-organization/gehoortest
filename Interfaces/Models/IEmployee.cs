namespace Interfaces.Models;

public interface IEmployee : IModel
{
    public IEmployeeLogin? EmployeeLogin { get; set; }
    public string? EmployeeNumber { get; set; }
    public string FirstName { get; set; }
    public string? Infix { get; set; }
    public string LastName { get; set; }
    public bool Active { get; set; }
}
