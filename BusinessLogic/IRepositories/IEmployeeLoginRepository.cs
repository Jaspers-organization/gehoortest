using BusinessLogic.Models;

namespace BusinessLogic.IRepositories;

public interface IEmployeeLoginRepository
{
    public EmployeeLogin? GetByEmailAndActive(string email);
}
