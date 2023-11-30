using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class TestService
    {
        private ITestRepository testRepository { get; set; }

        public TestService(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public ITest? GetTest(int targetAudienceId)
        {
           return testRepository.Get(targetAudienceId);
        }
    }
}
