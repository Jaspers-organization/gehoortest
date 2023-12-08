using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using gehoortest_application.Repository;

namespace DataAccess.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly Repository repository = new Repository();

        public Test CreateTest() => new Test();


        public void DeleteTest(Test test)
        {
            throw new NotImplementedException();
        }

        public Test? GetActiveTest()
        {
            throw new NotImplementedException();
        }

        public List<Test> GetAllTests()
        {
            throw new NotImplementedException();
        }

        public Test? GetTestById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Test? GetTestByTargetAudienceId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TestProjection>? GetTestProjectionsByTargetAudienceId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveTest(Test test)
        {
            repository.Tests.Add(test);
            repository.SaveChanges();
            //repository.SaveChangesWithIdentityInsert<TestDTO>();

        }

        public void UpdateTest(Test test)
        {
            throw new NotImplementedException();
        }
    }
}
