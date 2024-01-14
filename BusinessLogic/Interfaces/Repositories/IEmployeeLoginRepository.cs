using BusinessLogic.Models;

namespace BusinessLogic.Interfaces.Repositories;

public interface IEmployeeLoginRepository
{
    public EmployeeLogin? GetByEmailAndActive(string email);
    public EmployeeLogin? GetByEmployeeId(Guid id);
    public bool EmailExists(string email);
}
