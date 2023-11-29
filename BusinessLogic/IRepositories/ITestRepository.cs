using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System.Collections.ObjectModel;

namespace BusinessLogic.IRepositories;

public interface ITestRepository
{
    ITest GetTest(int  id);
    void Update(ITest test);
    void Create(ITest test);
    ObservableCollection<TestProjection> GetTestsProjectionForAudience(int id);
}
