using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DataTransferObjects;

[Table("text_question")]
public class TextQuestionDTO
{
    [Column("id")]
    public int Id { get; set; }

    [Column("question")]
    public string Question { get; set; }

    [Column("is_multi_select")]
    public bool IsMultiSelect { get; set; }

    [Column("has_input_field")]
    public bool HasInputField { get; set; }

    [Column("question_number")]
    public int QuestionNumber { get; set; }
       

    [Column("test_id")]
    public int TestId { get; set; }
    public virtual TestDTO Test { get; set; }

    public virtual ICollection<TextQuestionOptionDTO>? Options { get; set; }
}
