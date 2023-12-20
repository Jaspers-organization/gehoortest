using BusinessLogic.Interfaces;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Projections;

namespace BusinessLogic.Services;

public class LoginService
{
    #region dependencies
    private IEmployeeLoginRepository repository;
    private IHashingService hashingService;
    #endregion

    public LoginService(IEmployeeLoginRepository repository, IHashingService hashingService)
    {
        this.repository = repository;
        this.hashingService = hashingService;
    }

    public EmployeeProjection? Login(string email, string password) 
    {
        EmployeeLogin? employeeLogin = repository.GetByEmailAndActive(email);
        if (employeeLogin == null)
        {
            return null;
        }

        bool isPasswordCorrect = CheckLogin(password, employeeLogin.Salt, employeeLogin.Password);
        if (!isPasswordCorrect)
        {
            return null;
        }

        Employee employee = employeeLogin.Employee!;
        return new EmployeeProjection()
        {
            Id = employee.Id,
            EmployeeNumber = employee.EmployeeNumber,
            FullName = employee.FullName,
            Role = employee.AccountType,
        };
    }

    private bool CheckLogin(string password, string salt, string passwordToVerify)
    {
        return hashingService.VerifyPassword(password, salt, passwordToVerify);
    }
}