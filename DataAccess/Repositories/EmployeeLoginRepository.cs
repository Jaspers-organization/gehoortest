using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class EmployeeLoginRepository : IEmployeeLoginRepository
{
    private Repository repository => new Repository();

    public EmployeeLogin? GetByEmailAndActive(string email)
    {
        return repository.EmployeeLogins
            .Include(el => el.Employee)
            .FirstOrDefault(el => el.Email == email);
    }

    public EmployeeLogin? GetByEmployeeId(Guid id)
    {
        return repository.EmployeeLogins.Where(el => el.EmployeeId == id).FirstOrDefault();
    }
    public void DeleteEmployeeLogin(EmployeeLogin entity)
    {
        repository.EmployeeLogins.Remove(entity);
        repository.SaveChanges();
    }
    public void SaveEmployeeLogin(EmployeeLogin entity)
    {
        repository.EmployeeLogins.Add(entity);
        repository.SaveChanges();
    }

    public void UpdateEmployeeLogin(EmployeeLogin entity)
    {
        repository.Update(entity);
        repository.SaveChanges();
    }
}
