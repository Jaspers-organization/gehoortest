using gehoortest.application_Repository.Models.LoginData_Management;
using gehoortest_application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gehoortest.application_Repository.Repositorys.LoginData_Management_DbContext
{
    internal class Employee_LoginDbContext : Repository<Employee_Login>
    {

        public Employee_LoginDbContext(string connectionString) : base(connectionString)
        {
        }
    }

}
