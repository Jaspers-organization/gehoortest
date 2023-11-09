using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("text_question")]
internal sealed class TextQuestion : ITextQuestion
{
    [Column("id")]
    public int Id { get; set; }

    [Column("test_id")]
    public int TestId { get; set; }

    [Column("question_number")]
    public int QuestionNumber { get; set; }

    [Column("question")]
    public string Question { get; set; }

    public List<ITextQuestionOption> TextQuestionOption { get; set; }

    [Column("is_multiple_select")]
    public bool IsMultipleSelect { get; set; }

    [Column("has_input_field")]
    public bool HasInputField { get; set; }

    [Column("image")]
    public string? Image { get; set; }
}
