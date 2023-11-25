using Service.IModels;

namespace Service.IRepositories;

public interface ITestRepository
{
    public string Title { get; set; }
    void Get(int  id);
    void Update(ITest test);
    void Create(ITest test);
}
