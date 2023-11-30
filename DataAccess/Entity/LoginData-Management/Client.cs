using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.LoginData_Management;

[Table("client")]
public class Client
{
    [Column("id")]
    public int Id{ get; set; }

    [Column("full_name")]
    public string? FullName { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("password")]
    public string? Password { get; set; }
}
