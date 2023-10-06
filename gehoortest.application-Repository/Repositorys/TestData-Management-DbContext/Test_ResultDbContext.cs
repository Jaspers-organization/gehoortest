using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest_application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gehoortest.application_Repository.Repositorys.TestData_Management_DbContext
{
    public class Test_ResultDbContext : Repository<Test_Result>
    {
        
        public Test_ResultDbContext(string connectionString) : base(connectionString)
        {
        }
    }

}
