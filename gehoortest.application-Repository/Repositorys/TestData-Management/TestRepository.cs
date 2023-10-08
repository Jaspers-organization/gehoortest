using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.TestData_Management;

public class TestRepository : Repository
{
    public TestRepository(string connectionString) : base(connectionString)
    {
    }

    public ObservableCollection<Test> GetAllActiveTests()
    {
        return Get<Test>(t => t.Active);
    }

}
