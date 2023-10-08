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

    //example for getting data as anoynmous type (if you want to select certain fields.)
    public List<object> GetAllActiveTests(int id)
    {
        return Get<Test>(t => t.Id == id)
            .Select(v => new { v.Active, v.TestData })
            .ToList<object>();
    }

    public List<Test> GetAllActiveTests()
    {
        return Get<Test>(t => t.Active);
    }

}
