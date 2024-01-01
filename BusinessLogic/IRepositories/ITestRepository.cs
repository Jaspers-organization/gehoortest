using BusinessLogic.Models;
using BusinessLogic.Projections;

namespace BusinessLogic.IRepositories;

public interface ITestRepository
{
    public Test? GetTestById(Guid id);
    public void UpdateTest(Test test);
    public Test CreateTest();
    public void SaveTest(Test test);
    public void DeleteTest(Test test);
    public List<Test> GetAllTests();
    public List<TestProjection>? GetTestProjectionsByTargetAudienceId(Guid id);
    public Test? GetTestByTargetAudienceIdAndActive(Guid id);
    public Test? GetActiveByTargetAudienceId(Guid id);
    public List<TestProjection>? GetTestProjectionsByNoTargetAudience();
}
