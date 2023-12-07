using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("text_question_option")]
public class TextQuestionOptionDTO
{
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Column("option", TypeName = "varchar(50)")]
    public string Option { get; set; }

    [Column("text_question_id", TypeName = "int")]
    public int TextQuestionId { get; set; }
    public virtual TextQuestionDTO? TextQuestion { get; set; }
}
