using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("test_result")]
public class TestResultDTO
{
    [Column("id", TypeName = "")]
    public int Id { get; set; }

    [Column("branch_id", TypeName = "")]
    internal int BranchId { get; set; }

    [Column("client_id", TypeName = "")]
    internal int? ClientId { get; set; }

    [Column("start_date_time", TypeName = "")]
    public DateTime StartDate { get; set; }

    [Column("duration", TypeName = "")]
    public int TestDuration { get; set; }

    [Column("test_answers", TypeName = "")]
    public string? TestAnswers { get; set; }

    //public virtual IClient? Client { get; set; }

    //public virtual IBranch Branch { get; set; }
}
