using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("branch")]
internal sealed class Branch : IBranch
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
}
