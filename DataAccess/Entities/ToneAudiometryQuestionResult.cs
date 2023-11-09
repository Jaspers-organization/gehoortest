using Interfaces.Enums;
using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("tone_audiometry_question_result")]
internal sealed class ToneAudiometryQuestionResult : IToneAudiometryQuestionResult
{
    [Column("id")]
    public int Id { get; set; }

    [Column("test_result_id")]
    public int TestResultId { get; set; }

    [Column("frequency")]
    public int Frequency { get; set; }

    [Column("ear")]
    public Ear Ear { get; set; }

    [Column("starting_decibels")]
    public int StartingDecibels { get; set; }

    [Column("answer")]
    public int Answer { get; set; }
}
