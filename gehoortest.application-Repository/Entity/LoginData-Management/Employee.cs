using gehoortest.application_Repository.Models.BusinessData_Management;
using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.LoginData_Management;

[Table("employee")]
public class Employee
{
    public enum role { employee, adminstrator } // in de interface

    [Column("id")]
    public int Id { get; set; }

    [Column("employee_number")]
    public string? EmployeeNumber { get; set; }

    [Column("fullname")]
    public string? Fullname { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("password")]
    public string? Password { get; set; }

    [Column("password_hash")]
    public int? BranchId { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    public virtual Branch? Branch { get; set; }
}
