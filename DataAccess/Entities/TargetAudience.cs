using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("target_audience")]
internal sealed class TargetAudience : ITargetAudience
{
    [Column("id")]
    public int Id { get; set; }

    [Column("from")]
    public int From { get; set; }

    [Column("to")]
    public int To { get; set; }

    [Column("label")]
    public string Label { get; set; }
}
