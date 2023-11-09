using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("text_question_answer_result")]
internal sealed class TextQuestionAnswerResult : ITextQuestionAnswerResult
{
    [Column("id")]
    public int Id { get; set; }

    [Column("text_question_result_id")]
    public int TestQuestionResultId { get; set; }

    [Column("answer")]
    public string Answer { get; set; }
}
