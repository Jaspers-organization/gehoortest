using BusinessLogic.Models;

namespace BusinessLogic.IRepositories;

public interface IEmployeeLoginRepository
{
    public EmployeeLogin? GetByEmailAndActive(string email);
    public EmployeeLogin? GetByEmployeeId(Guid id);
    public bool EmailExists(string email);
}
