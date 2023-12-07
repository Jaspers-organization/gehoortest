using System.ComponentModel.DataAnnotations.Schema;
namespace BusinessLogic.DataTransferObjects;

[Table("tone_audiometry_question")]
public class ToneAudiometryQuestionDTO
{
    [Column("id")]
    public int Id { get; set; }

    [Column("frequency")]
    public int Frequency { get; set; }

    [Column("starting_decibels")]
    public int StartingDecibels { get; set; }

    [Column("question_number")]
    public int QuestionNumber { get; set; }


    public int TestId {  get; set; }
    public virtual TestDTO? Test { get; set; }
}
