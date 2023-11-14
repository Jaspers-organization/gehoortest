using gehoortest.application_Repository.Models.LoginData_Management;
using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.TestData_Management;

[Table("test")]
public class Test
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("target_audience_id")]
    public int TargetAudienceId { get; set; }

    [Column("test_data")]
    public string? TestData { get; set; }

    [Column("active")]
    public bool Active { get; set; }
       
    public virtual Employee? Employee { get; set; }

    public virtual TargetAudience? TargetAudience { get; set; }

}
