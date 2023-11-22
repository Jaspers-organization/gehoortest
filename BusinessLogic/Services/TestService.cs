using Service.IModels;
using Service.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services;

public class TestService
{
    private ITestRepository testRepository;
    public TestService(ITestRepository testRepository)
    {
        this.testRepository = testRepository;
    }
    public ITest GetTest(string id)
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
