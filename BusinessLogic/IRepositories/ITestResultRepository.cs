using BusinessLogic.Models;

namespace BusinessLogic.IRepositories;

public interface ITestResultRepository
{
    public void Store(TestResult testResult);
}
