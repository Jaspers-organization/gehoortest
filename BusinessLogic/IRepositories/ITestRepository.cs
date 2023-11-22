using Service.IModels;

namespace Service.IRepositories;

public interface ITestRepository
{
    void Get(int  id);
    void Update(ITest test);
    void Create(ITest test);
}
