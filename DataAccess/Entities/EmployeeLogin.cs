using Interfaces.Enums;
using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("employee")]
internal sealed class EmployeeLogin : IEmployeeLogin
{
    [Column("id")]
    public int Id { get; set; }

    [Column("mail")]
    public string Mail { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("role")]
    public Role Role { get; set; }
}
