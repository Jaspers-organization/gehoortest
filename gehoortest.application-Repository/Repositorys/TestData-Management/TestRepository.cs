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
    public List<object> GetAllActiveTests(byte amount)
    {
        return Get<TargetAudience>(t => t.From < amount)
            .Select(v => new { v.From, v.To })
            .ToList<object>();
    }

    public List<Test> GetAllActiveTests()
    {
        return Get<Test>(t => t.Active);
    }

}
