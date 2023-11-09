using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("text_question_result")]
internal sealed class TextQuestionResult : ITextQuestionResult
{
    [Column("id")]
    public int Id { get; set; }

    [Column("test_result_id")]
    public int TestResultId { get; set; }

    [Column("question")]
    public string Question { get; set; }

    public List<ITextQuestionOptionResult> TextQuestionOptionResults { get; set; }

    public List<ITextQuestionAnswerResult> TextQuestionAnswerResults { get; set; }
}
