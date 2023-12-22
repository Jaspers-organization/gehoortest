using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Projections;

namespace BusinessLogic.Services;

public class EmployeeService
{
    private IEmployeeRepository employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository) => this.employeeRepository = employeeRepository;

    public Employee GetEmployeeById(Guid id)
    {
        return employeeRepository.GetEmployeeById(id);
    }
    public List<EmployeeProjection> GetEmployeesProjections()
    {
        return employeeRepository.GetAllEmployeeProjections();
    }    
    public void ProcessEmployee(Employee employee, bool newEmployee)
    {
        if (newEmployee)
        {
            SaveEmployee(employee);
        }
        else
        {
            UpdateEmployee(employee);
        }
    }
    private void SaveEmployee(Employee employee)
    {
        employeeRepository.SaveEmployee(employee);
    }
    private void UpdateEmployee(Employee employee)
    {
        employeeRepository.UpdateEmployee(employee);

    }
    private void DeleteEmployee(Guid id)
    {
        throw new NotImplementedException();
    }
    public Employee CreateEmployee()
    {
        return new Employee();
    }
}
