using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.BusinessData_Management;

[Table("employee_branch")]
public class Employee_Branch
{

    [Column("employee_id")]
    public int Employee_id { get; set; }
    
    [Column("branch_id")]
    public int Branch_id { get; set; }
}
