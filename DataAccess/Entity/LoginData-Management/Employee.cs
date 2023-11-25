using DataAccess.Models.BusinessData_Management;
using BusinessLogic.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.LoginData_Management;

[Table("employee")]
public class Employee : IEmployee
{
    public enum role { employee, adminstrator } // in de interface

    [Column("id")]
    public int Id { get; set; }

    [Column("employee_number")]
    public string? EmployeeNumber { get; set; }
    

    public int EmployeeId { get ; set ; }
    public string? FirstName { get ; set ; }
    public string? LastName { get ; set ; }
    public string? Infix { get ; set ; }


    public string Fullname
    {
        get
        {
            if (string.IsNullOrEmpty(Infix))
            {
                return $"{FirstName} {LastName}";
            }
            else
            {
                return $"{FirstName} {Infix} {LastName}";
            }
        }
        
    }

}
