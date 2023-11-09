using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("text_question_option_result")]
internal sealed class TextQuestionOptionResult : ITextQuestionOptionResult
{
    [Column("id")]
    public int Id { get; set; }

    [Column("text_question_result_id")]
    public int TestQuestionResultId { get; set; }

    [Column("option")]
    public string Option { get; set; }
}
