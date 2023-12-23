using BusinessLogic.BusinessRules;
using BusinessLogic.Interfaces;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using UserInterface.Stores;

namespace BusinessLogic.Services;

public class EmployeeService
{
    private IEmployeeRepository employeeRepository;
    private IEmployeeLoginRepository employeeLoginRepository;
    private IHashingService hashingService;

    public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeLoginRepository employeeLoginRepository, IHashingService hashingService) : this(employeeRepository, employeeLoginRepository)
    {
        this.hashingService = hashingService;
    }

    public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeLoginRepository employeeLoginRepository) : this(employeeRepository)
    {
        this.employeeLoginRepository = employeeLoginRepository;
    }

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }
    public Employee GetEmployeeById(Guid id)
    {
        return employeeRepository.GetEmployeeById(id);
    }
    public List<EmployeeProjection> GetEmployeesProjections()
    {
        return employeeRepository.GetAllEmployeeProjections();
    }

    private void SaveEmployee(Employee employee)
    {
        employeeRepository.SaveEmployee(employee);
    }
    private void UpdateEmployee(Employee employee)
    {
        employeeRepository.UpdateEmployee(employee);
    }
    public void DeleteEmployee(Employee employee)
    {
        employeeRepository.DeleteEmployee(employee);
    }
    public Employee CreateEmployee()
    {
        return new Employee();
    }
    public bool AbleToDeleteEmployee(Guid id)
    {
        return employeeRepository.AbleToDeleteEmployee(id);
    }
    public EmployeeLogin GetEmployeeLoginById(Guid id)
    {
        return employeeLoginRepository.GetByEmployeeId(id);
    }
    public EmployeeLogin CreateEmployeeLogin()
    {
        return new EmployeeLogin();
    }

    public void ProcessEmployee(Employee employee, bool newEmployee, bool passwordChanged)
    {
        EmployeeBusinessRules.ValidateEmployee(employee);
        if (employeeLoginRepository.EmailExists(employee.EmployeeLogin.Email))
            throw new Exception(ErrorMessageStore.ErrorEmailInUse);

        //check if email exists
        if (passwordChanged || newEmployee)
        {
            (string salt, string hashedPassword) = PasswordService.GenerateLogin(employee.EmployeeLogin.Password, hashingService);
            employee.EmployeeLogin.Salt = salt;
            employee.EmployeeLogin.Password = hashedPassword;
        }
        if (newEmployee)
        {
            SaveEmployee(employee);
        }
        else
        {
            UpdateEmployee(employee);
        }
    }

    public void ToggleActiveStatus(Guid id, Guid LoggedInEmployee)
    {
        EmployeeLogin? employeeLogin = GetEmployeeLoginById(id);
        Employee? employee = GetEmployeeById(id);

        if (employeeLogin == null) throw new Exception(ErrorMessageStore.ErrorEmployeeStatusGeneric);

        if (employee.Id == LoggedInEmployee) throw new Exception(ErrorMessageStore.ErrorEmployeeStatus);
        employeeLogin.Active = !employeeLogin.Active;

        employee.EmployeeLogin = employeeLogin;
        employeeRepository.UpdateEmployee(employee);
    }
}
