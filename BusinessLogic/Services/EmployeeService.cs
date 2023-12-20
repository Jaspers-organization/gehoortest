using BusinessLogic.IRepositories;
using BusinessLogic.Models;

namespace BusinessLogic.Services;

public class EmployeeService
{
    private IEmployeeRepository employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository) => this.employeeRepository = employeeRepository;

    public Employee GetEmployeeById(Guid id)
    {
        return employeeRepository.GetEmployeeById(id);
    }
}
