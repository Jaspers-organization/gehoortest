using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Projections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services;

public class TestService
{
    private ITestRepository testRepository;

    public TestService(ITestRepository testRepository)
    {
        this.testRepository = testRepository;
    }
    public ITest GetTest(ITargetAudience targetAudience)
    {
        return testRepository.GetTest(targetAudience.Id);
    }
    public ObservableCollection<TestProjection> GetTestsProjectionForAudience(int id)
    {
        return testRepository.GetTestsProjectionForAudience(id);
    }

    public void UpdateTest(ITest test)
    {
        testRepository.Update(test);
    }
    public void CreateTest(ITest test)
    {
        testRepository.Create(test);
    }
}
