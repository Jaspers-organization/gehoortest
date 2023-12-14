using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class TestResultRepository : ITestResultRepository
{
    private readonly Repository repository = new Repository();

    public void Store(TestResult testResult)
    {
        repository.TestResults.Add(testResult);
        repository.SaveChanges();
    }
}
