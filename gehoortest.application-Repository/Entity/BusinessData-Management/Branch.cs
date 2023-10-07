using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.BusinessData_Management;
[Table("employee_branch")]
public class Branch
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }
}
