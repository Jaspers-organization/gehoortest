using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("employee_login")]
public class EmployeeLoginDTO 
{
    [Key]
    [Column("id", TypeName = "nvarchar(16)")]
    public Guid Id { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
    public bool Active { get; set; }
}
