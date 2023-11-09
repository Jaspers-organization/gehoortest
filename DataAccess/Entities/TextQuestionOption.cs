using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("text_question_option")]
internal sealed class TextQuestionOption : ITextQuestionOption
{
    [Column("id")]
    public int Id { get; set; }

    [Column("test_question_id")]
    public int TestQuestionId { get; set; }

    [Column("option")]
    public string Option { get; set; }
}
