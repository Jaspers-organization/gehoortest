using gehoortest.application_Repository.Models.BusinessData_Management;
using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gehoortest.application_Repository.Repositorys.BusinessData_Management_DbContext
{
    internal class BranchDbContext : Repository<Branch>
    {
        public BranchDbContext(string connectionString) : base(connectionString)
        {
        }
    }
}
