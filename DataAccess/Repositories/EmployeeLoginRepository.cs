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
}
