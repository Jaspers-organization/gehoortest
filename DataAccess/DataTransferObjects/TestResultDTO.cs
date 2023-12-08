using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("test_result")]
public class TestResultDTO
{
    [Key]
    [Column("id", TypeName = "nvarchar(16)")]
    public Guid Id { get; set; }

    [Column("branch_id", TypeName = "")]
    internal Guid BranchId { get; set; }

    [Column("client_id", TypeName = "")]
    internal Guid? ClientId { get; set; }

    [Column("start_date_time", TypeName = "")]
    public DateTime StartDate { get; set; }

    [Column("duration", TypeName = "")]
    public int TestDuration { get; set; }

    [Column("test_answers", TypeName = "")]
    public string? TestAnswers { get; set; }

    //public virtual IClient? Client { get; set; }

    //public virtual IBranch Branch { get; set; }
}
