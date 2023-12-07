using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DataTransferObjects;

[Table("employee")]
public class EmployeeDTO
{
    public enum role { employee, adminstrator } 

    [Column("id")]
    public int Id { get; set; }

    [Column("employee_number")]
    public string? EmployeeNumber { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("infix")]
    public string? Infix { get; set; }

    public virtual ICollection<TestDTO>? Tests { get; set; }
}
