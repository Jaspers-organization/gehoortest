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
    public int Employee_id { get; set; }

    [Column("test_data")]
    public string? Test_data { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }
}
