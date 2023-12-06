using BusinessLogic.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity.LoginData_Management;

public class EmployeeLogin : IEmployeeLogin
{
    public int Id { get; set; }
    public string Email { get; set ; }
    public string Password { get ; set ; }
    public bool Active { get ; set ; }
}
