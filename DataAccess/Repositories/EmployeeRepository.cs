using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using gehoortest_application.Repository;

namespace DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly Repository repository = new();

    public List<EmployeeProjection> GetAllEmployeeProjections()
    {
        List<Employee> employees = repository.Employees.ToList();

        if (employees == null || employees.Count == 0)
        {
            return new List<EmployeeProjection>();
        }

        List<EmployeeProjection> projections = employees.Select(employee => new EmployeeProjection
        {
            Id = employee.Id,
            AmountOfTests = AmountOfTestsPerEmployee(employee.Id),
            EmployeeNumber = employee.EmployeeNumber,
            FullName = employee.FullName,
            Role = employee.AccountType,
            Active = IsEmployeeActive(employee.Id),
        }).ToList();

        return projections;

    }
    private bool IsEmployeeActive(Guid id)
    {
        return repository.EmployeeLogins.Where(el => el.EmployeeId == id).FirstOrDefault().Active;
    }
    public int AmountOfTestsPerEmployee(Guid id)
    {
        return repository.Employees.Where(e => e.Id == id).SelectMany(e => e.Tests).Count();
    }
    public Employee GetEmployeeById(Guid id)
    {
        return repository.Employees.FirstOrDefault(e => e.Id == id);
    }

    public void SaveEmployee(Employee entity)
    {
        repository.Employees.Add(entity);
        repository.SaveChanges();
    }
    public bool AbleToDeleteEmployee(Guid id)
    {
        return !repository.Employees.Where(e => e.Id == id).SelectMany(e => e.Tests).Any();
    }

    public void DeleteEmployee(Employee entity)
    {
        repository.Employees.Remove(entity);
        repository.SaveChanges();
    }

    public void UpdateEmployee(Employee entity)
    {
        repository.Employees.Update(entity);
        repository.SaveChanges();
    }
}
