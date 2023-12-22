using BusinessLogic.Interfaces;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;

namespace BusinessLogic.Services;

public class EmployeeLoginService
{
    private IEmployeeLoginRepository employeeLoginRepository;
    private IHashingService hashingService;

    public EmployeeLoginService(IEmployeeLoginRepository employeeLoginRepository, IHashingService hashingService)
    { 
        this.employeeLoginRepository = employeeLoginRepository; 
        this.hashingService = hashingService; 
    }
    public EmployeeLoginService(IEmployeeLoginRepository employeeLoginRepository)
    {
        this.employeeLoginRepository = employeeLoginRepository;
    }
    public EmployeeLogin GetByEmployeeId(Guid id)
    {
        return employeeLoginRepository.GetByEmployeeId(id);
    }
    public EmployeeLogin CreateEmployeeLogin()
    {
        return new EmployeeLogin();
    }
    private (string salt, string hashedPassword) GenerateLogin(string password)
    {
        string salt = hashingService.GenerateSalt();
        return (salt, hashingService.HashPassword(password, salt)); 
    }

    public void ProcessEmployeeLogin(EmployeeLogin employeeLogin, bool newEmployeeLogin, bool passwordChanged)
    {
        if (passwordChanged || newEmployeeLogin)
        {
            (string salt, string hashedPassword) = GenerateLogin(employeeLogin.Password);
            employeeLogin.Salt = salt;
            employeeLogin.Password = hashedPassword;
        }

        if (newEmployeeLogin){
            SaveEmployeeLogin(employeeLogin);
        }
        else
        {
            UpdateEmployeeLogin(employeeLogin);
        }
    }
    public void DeleteEmployeeLogin(EmployeeLogin employeeLogin)
    {
        employeeLoginRepository.DeleteEmployeeLogin(employeeLogin);
    }
    private void SaveEmployeeLogin(EmployeeLogin employeeLogin)
    {
        employeeLoginRepository.SaveEmployeeLogin(employeeLogin);
    }
    private void UpdateEmployeeLogin(EmployeeLogin employeeLogin)
    {
        employeeLoginRepository.UpdateEmployeeLogin(employeeLogin);
    }
}
