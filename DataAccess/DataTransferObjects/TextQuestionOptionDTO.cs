using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("text_question_option")]
public class TextQuestionOptionDTO
{
    [Key]
    [Column("id", TypeName = "nvarchar(16)")]
    public Guid Id { get; set; }

    [Column("option", TypeName = "varchar(50)")]
    public string Option { get; set; }

    [Column("text_question_id", TypeName = "int")]
    public Guid TextQuestionId { get; set; }
    public virtual TextQuestionDTO? TextQuestion { get; set; }
}
