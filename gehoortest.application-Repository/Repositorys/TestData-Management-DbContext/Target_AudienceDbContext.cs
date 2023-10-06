using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest_application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gehoortest.application_Repository.Repositorys.TestData_Management_DbContext
{
    public class Target_AudienceDbContext : Repository<Target_Audience>
    {
        
        public Target_AudienceDbContext(string connectionString) : base(connectionString)
        {
        }
    }

}
