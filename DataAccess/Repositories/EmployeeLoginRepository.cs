using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using gehoortest_application.Repository;
using Microsoft.EntityFrameworkCore;
using System;

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
    public bool EmailExists(string email)
    {
        return repository.EmployeeLogins.Where(el => el.Email == email).Any();
    }
}
