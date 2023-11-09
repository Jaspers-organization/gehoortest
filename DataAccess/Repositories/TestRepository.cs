using DataAccess.Entities;
using Interfaces.Models;

namespace DataAccess.Repositories;

public class TestRepository : Repository
{
    public TestRepository(string connectionString) : base(connectionString)
    {
    }

    //example for getting data as anoynmous type (if you want to select certain fields.)
    //public List<object> GetAllActiveTests(int id)
    //{
    //    return Get<Test>(t => t.Id == id)
    //        .Select(v => new { v.Active, v.TestData })
    //        .ToList<object>();
    //}

    public List<ITest> GetAllActiveTests()
    {
        return Get<ITest>(t => t.Active);
    }

}
