using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.LoginData_Management;

[Table("employee_login")]
public class EmployeeLoginRepository
{
    [Column("id")]
    public int id { get; set; }

    [Column("fullname")]
    public string? Fullname { get; set; }

    [Column("employee_number")]
    public string? Employee_number { get; set; }

    [Column("password_hash")]
    public string? Password_hash { get; set; }

    [Column("password_salt")]
    public string? Password_salt { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("active")]
    public bool Active { get; set; }
}
