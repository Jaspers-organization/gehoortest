using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("client")]
public class ClientDTO
{
    [Column("id")]
    public int Id { get; set; }

    [Column("full_name")]
    public string? FullName { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("password")]
    public string? Password { get; set; }
}
