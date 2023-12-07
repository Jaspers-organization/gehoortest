using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("target_audience")]
public class TargetAudienceDTO
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id", TypeName = "int")]
    public int Id { get; set; }

    [Column("from", TypeName = "int")]
    public int From { get; set; }

    [Column("to", TypeName = "int")]
    public int To { get; set; }

    [Column("label", TypeName = "varchar(50)")]
    public string Label { get; set; }

    public virtual ICollection<TestDTO>? Tests { get; set; }

}
