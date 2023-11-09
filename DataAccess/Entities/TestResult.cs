using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("test_result")]
internal sealed class TestResult : ITestResult
{
    [Column("id")]
    public int Id { get; set; }

    [Column("target_audience_id")]
    public int TargetAudienceId { get; set; }
    public ITargetAudience TargetAudience { get; set; }

    [Column("branch_id")]
    public int BranchId { get; set; }
    public IBranch Branch { get; set; }

    [Column("test_date_time")]
    public DateTime TestDateTime { get; set; }

    [Column("duration")]
    public int Duration { get; set; }

    public List<ITextQuestionResult> TextQuestionResults { get; set; }

    public List<IToneAudiometryQuestionResult> ToneAudiometryQuestions { get; set; }
}
