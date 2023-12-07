using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DataTransferObjects;

[Table("employee_login")]
public class EmployeeLoginDTO 
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; }
}
