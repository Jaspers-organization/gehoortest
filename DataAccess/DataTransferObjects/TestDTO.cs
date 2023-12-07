using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("test")]
public class TestDTO
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Column("title", TypeName = "varchar(50)")]
    public string Title { get; set; }

    [Column("active", TypeName = "bit")]
    public bool Active { get; set; }

    [Column("target_audience_id", TypeName = "int")]
    public int TargetAudienceId { get; set; }

    public virtual TargetAudienceDTO? TargetAudience { get; set; }

    [Column("employee_id", TypeName = "int")]
    public int EmployeeId { get; set; }

    public virtual EmployeeDTO? Employee { get; set; }

    public virtual ICollection<TextQuestionDTO>? TextQuestions { get; set; }

    public virtual ICollection<ToneAudiometryQuestionDTO>? ToneAudiometryQuestions { get; set; }
}
