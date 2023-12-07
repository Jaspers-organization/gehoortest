using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DataTransferObjects;

[Table("target_audience")]
public class TargetAudienceDTO
{
    [Column("id")]
    public int Id { get; set; }

    [Column("from")]
    public int From { get; set; }

    [Column("to")]
    public int To { get; set; }

    [Column("label")]
    public string? Label { get; set; }

    public virtual ICollection<TestDTO>? Tests { get; set; }

}
