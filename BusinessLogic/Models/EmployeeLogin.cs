using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class EmployeeLogin : IModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool Active { get; set; }

    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
