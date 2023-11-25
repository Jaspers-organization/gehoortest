using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using System;
using System.Collections.Generic;
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
        return null;
    }
    public void UpdateTest(ITest test)
    {

    }
    public void CreateTest(ITest test)
    {
        
    }
}
