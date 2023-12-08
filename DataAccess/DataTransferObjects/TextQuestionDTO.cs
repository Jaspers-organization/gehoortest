using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("text_question")]
public class TextQuestionDTO
{
    [Key]
    [Column("id", TypeName = "nvarchar(16)")]
    public Guid Id { get; set; }

    [Column("question", TypeName = "varchar(100)")]
    public string Question { get; set; }

    [Column("is_multi_select", TypeName = "bit")]
    public bool IsMultiSelect { get; set; }

    [Column("has_input_field", TypeName = "bit")]
    public bool HasInputField { get; set; }

    [Column("question_number", TypeName = "int")]
    public int QuestionNumber { get; set; }

    [Column("test_id", TypeName = "int")]
    public Guid TestId { get; set; }
    public virtual TestDTO Test { get; set; }

    public virtual ICollection<TextQuestionOptionDTO>? Options { get; set; }
}
