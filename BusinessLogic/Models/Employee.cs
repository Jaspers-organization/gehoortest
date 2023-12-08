using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class Employee : IModel
{
    public Guid Id { get; set; }
    public enum role { employee, adminstrator } // in de interface TODO
    public string? EmployeeNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Infix { get; set; }
    public string? Fullname { get; set; }

    public Guid TestId {  get; set; }
    public ICollection<Test>? Tests {  get; set; }
}
