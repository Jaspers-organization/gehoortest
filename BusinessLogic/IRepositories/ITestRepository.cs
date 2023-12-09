using BusinessLogic.Models;
using BusinessLogic.Projections;

namespace BusinessLogic.IRepositories;

public interface ITestRepository
{
    Test? GetTestById(Guid id);
    void UpdateTest(Test test);
    Test CreateTest();
    void SaveTest(Test test);
    void DeleteTest(Test test);
    List<Test> GetAllTests();
    List<TestProjection>? GetTestProjectionsByTargetAudienceId(Guid id);
    Test? GetTestByTargetAudienceIdAndActive(Guid id);
}
