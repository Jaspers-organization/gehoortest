
using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("tone_audiometry_question")]
internal sealed class ToneAudiometryQuestion : IToneAudiometryQuestion
{
    [Column("id")]
    public int Id { get; set; }

    [Column("test_id")]
    public int TestId { get; set; }

    [Column("question_number")]
    public int QuestionNumber { get; set; }

    [Column("frequency")]
    public int Frequency { get; set; }

    [Column("starting_decibels")]
    public int StartingDecibels { get; set; }
}
