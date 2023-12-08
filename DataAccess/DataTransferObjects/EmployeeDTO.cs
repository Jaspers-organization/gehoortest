using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("employee")]
public class EmployeeDTO
{
    //public enum role { employee, adminstrator } todo make conversion in fluent api to INT 

    [Key]
    [Column("id", TypeName = "nvarchar(16)")]
    public Guid Id { get; set; }

    [Column("employee_number", TypeName = "nvarchar(50)")]
    public string? EmployeeNumber { get; set; }

    [Column("first_name", TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }

    [Column("last_name", TypeName = "nvarchar(10)")]
    public string LastName { get; set; }

    [Column("infix", TypeName = "nvarchar(50)")]
    public string? Infix { get; set; }

    public virtual ICollection<TestDTO>? Tests { get; set; }
}
