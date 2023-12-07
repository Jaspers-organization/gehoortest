using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DataTransferObjects;

[Table("text_question_option")]
public class TextQuestionOptionDTO
{
    [Column("id")]
    public int Id { get; set; }

    [Column("option")]
    public string Option { get; set; }

    [Column("text_question_id")]
    public int TextQuestionId { get; set; }
    public virtual TextQuestionDTO? TextQuestion { get; set; }
}
