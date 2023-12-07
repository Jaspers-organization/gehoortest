using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DataTransferObjects;

[Table("test")]
public class TestDTO
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("target_audience_id")]
    public int TargetAudienceId { get; set; }
    public virtual TargetAudienceDTO? TargetAudience { get; set; }

    public virtual ICollection<TextQuestionDTO>? TextQuestions { get; set; }

    public virtual ICollection<ToneAudiometryQuestionDTO>? ToneAudiometryQuestions { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }
    public virtual EmployeeDTO? Employee { get; set; }

}
