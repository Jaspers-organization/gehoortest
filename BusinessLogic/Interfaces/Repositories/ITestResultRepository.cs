using BusinessLogic.Models;

namespace BusinessLogic.Interfaces.Repositories;

public interface ITestResultRepository
{
    public void Store(TestResult testResult);

    public TestResult GetById(Guid id);
}
