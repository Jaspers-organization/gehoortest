using BusinessLogic.Models;

namespace BusinessLogic.IRepositories;

public interface IEmployeeLoginRepository
{
    public EmployeeLogin? GetByEmailAndActive(string email);
    public EmployeeLogin? GetByEmployeeId(Guid id);
    public void SaveEmployeeLogin(EmployeeLogin entity);
    public void UpdateEmployeeLogin(EmployeeLogin entity);
    public void DeleteEmployeeLogin(EmployeeLogin entity);
}
