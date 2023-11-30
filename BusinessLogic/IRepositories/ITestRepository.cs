using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System.Collections.ObjectModel;

namespace BusinessLogic.IRepositories;

public interface ITestRepository
{
    ITest GetTest(int  id);
    void UpdateTest(ITest test);
    ITest CreateTest();
    void SaveTest(ITest test);
    void DeleteTest(ITest test);
    ObservableCollection<TestProjection> GetTestsProjectionForAudience(int id);
}
