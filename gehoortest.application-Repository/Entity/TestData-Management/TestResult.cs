using gehoortest.application_Repository.Models.BusinessData_Management;
using gehoortest.application_Repository.Models.LoginData_Management;
using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.TestData_Management;
[Table("test_result")]
public class TestResult
{
    [Column("id")]
    public int Id { get; set; }

    [Column("branch_id")]
    public int BranchId { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("start_date_time")]
    public DateTime StartDate { get; set; }

    [Column("duration")]
    public int TestDuration { get; set; }

    [Column("test_answers")]
    public string? Test_answers { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Branch? Branch { get; set; }
}
