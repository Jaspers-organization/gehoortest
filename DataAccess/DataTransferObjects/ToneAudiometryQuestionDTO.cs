using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.DataTransferObjects;

[Table("tone_audiometry_question")]
public class ToneAudiometryQuestionDTO
{
    [Key]
    [Column("id", TypeName = "nvarchar(16)")]
    public Guid Id { get; set; }

    [Column("frequency", TypeName = "int")]
    public int Frequency { get; set; }

    [Column("starting_decibels", TypeName = "int")]
    public int StartingDecibels { get; set; }

    [Column("question_number", TypeName = "int")]
    public int QuestionNumber { get; set; }

    [Column("test_id", TypeName = "int")]
    public Guid TestId {  get; set; }

    public virtual TestDTO? Test { get; set; }
}
