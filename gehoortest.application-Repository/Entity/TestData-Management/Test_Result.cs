using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.TestData_Management;
[Table("test_result")]
public class Test_Result
{
    [Column("id")]
    public int Id { get; set; }

    [Column("test_id")]
    public int Test_id { get; set; }

    [Column("branch_id")]
    public int Branch_id { get; set; }

    [Column("client_id")]
    public int Client_id { get; set; }

    [Column("start_date_time")]
    public DateTime Start_date_time { get; set; }

    [Column("test_duration")]
    public int Test_duration { get; set; }

    [Column("test_answers")]
    public string? Test_answers { get; set; }
}
