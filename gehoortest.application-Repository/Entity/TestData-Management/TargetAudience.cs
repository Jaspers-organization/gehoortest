using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.TestData_Management;

[Table("target_audience")]
public class TargetAudience
{
    [Column("id")]
    public int Id { get; set; }

    [Column("from")]
    public byte From { get; set; }

    [Column("to")]
    public byte To { get; set; }

    [Column("label")]
    public string? Label { get; set; }
}
