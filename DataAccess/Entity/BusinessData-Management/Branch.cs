using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.BusinessData_Management;

[Table("branch")]
public class Branch
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; } 
}
