using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess.Repositories;

public class TestResultRepository : ITestResultRepository
{
    private readonly Repository repository = new Repository();

    public void Store(TestResult testResult)
    {
        repository.TestResults.Add(testResult);
        repository.SaveChanges();
    }

    public TestResult GetById(Guid id)
    {
        return repository.TestResults
            .Include(tr => tr.ToneAudiometryQuestions)
            .First(tr => tr.Id == id);
    }
}
