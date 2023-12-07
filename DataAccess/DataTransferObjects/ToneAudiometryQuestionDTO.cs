using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.DataTransferObjects;

[Table("tone_audiometry_question")]
public class ToneAudiometryQuestionDTO
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Column("frequency", TypeName = "int")]
    public int Frequency { get; set; }

    [Column("starting_decibels", TypeName = "int")]
    public int StartingDecibels { get; set; }

    [Column("question_number", TypeName = "int")]
    public int QuestionNumber { get; set; }

    [Column("test_id", TypeName = "int")]
    public int TestId {  get; set; }

    public virtual TestDTO? Test { get; set; }
}
