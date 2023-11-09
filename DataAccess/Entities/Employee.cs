using Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("employee")]
internal sealed class Employee : IEmployee
{
    [Column("id")]
    public int Id { get; set; }

    [Column("employee_login_id")]
    public int EmployeeLoginId { get; set; }
    public IEmployeeLogin? EmployeeLogin { get; set; }

    [Column("employee_number")]
    public string? EmployeeNumber { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("infix")]
    public string? Infix { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    [Column("active")]
    public bool Active { get; set; }
}
