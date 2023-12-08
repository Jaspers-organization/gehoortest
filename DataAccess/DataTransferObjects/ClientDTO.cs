using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataTransferObjects;

[Table("client")]
public class ClientDTO
{
    [Key]
    [Column("id", TypeName = "nvarchar(16)")]
    public Guid Id { get; set; }

    [Column("full_name", TypeName = "nvarchar(50)")]
    public string? FullName { get; set; }

    [Column("email", TypeName = "nvarchar(50)")]
    public string? Email { get; set; }

    [Column("password", TypeName = "nvarchar(128)")]
    public string? Password { get; set; }
}
