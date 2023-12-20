using BusinessLogic.Models;

namespace BusinessLogic.IRepositories;

public interface IEmployeeRepository
{
    public Employee GetEmployeeById(Guid id);
}
