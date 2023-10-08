using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.LoginData_Management;

[Table("client_login")]
public class ClientLoginRepository
{
    [Column("id")]
    public int Id{ get; set; }

    [Column("password_hash")]
    public string? Password_hash { get; set; }

    [Column("password_salt")]
    public string? Password_salt { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("active")]
    public bool Active { get; set; }
}
