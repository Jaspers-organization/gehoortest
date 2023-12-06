using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System.Collections.ObjectModel;

namespace BusinessLogic.IRepositories;

public interface ITestRepository
{
    ITest GetTest(int id);
    ITest? GetTestById(int id);
    ITest? GetActiveTest();
    void UpdateTest(ITest test);
    ITest CreateTest();
    void SaveTest(ITest test);
    void DeleteTest(ITest test);
    List<ITest> GetAllTests();
    List<ITest>? GetTestsByTargetAudienceId(int id);
}
