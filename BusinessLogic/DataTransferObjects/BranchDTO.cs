using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.DataTransferObjects;

[Table("branch")]
public class BranchDTO
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }
}
