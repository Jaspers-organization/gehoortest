using BusinessLogic.IModels;
using BusinessLogic.Projections;

namespace BusinessLogic.IRepositories;

public interface ITestRepository
{
    ITest? GetTestById(Guid id);
    ITest? GetActiveTest();
    void UpdateTest(ITest test);
    ITest CreateTest();
    void SaveTest(ITest test);
    void DeleteTest(ITest test);
    List<ITest> GetAllTests();
    List<TestProjection>? GetTestProjectionsByTargetAudienceId(Guid id);
    ITest? GetTestByTargetAudienceId(Guid id);
}
