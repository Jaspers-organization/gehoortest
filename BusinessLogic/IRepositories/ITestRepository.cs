using BusinessLogic.IModels;
using BusinessLogic.Projections;

namespace BusinessLogic.IRepositories;

public interface ITestRepository
{
    ITest? GetTestById(int id);
    ITest? GetActiveTest();
    void UpdateTest(ITest test);
    ITest CreateTest();
    void SaveTest(ITest test);
    void DeleteTest(ITest test);
    List<ITest> GetAllTests();
    List<TestProjection>? GetTestProjectionsByTargetAudienceId(int id);
    ITest? GetTestByTargetAudienceId(int id);
}
