using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using UserInterface.ViewModels.Modals;

namespace UserInterface.ViewModels;

internal class EmployeeManagementViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore navigationStore;
    private readonly TestService testService;
    private readonly TargetAudienceService targetAudienceSerivce;
    private readonly EmployeeService employeeService;
    private readonly EmployeeLoginService employeeLoginService;

    private readonly bool isNewEmployee;
    #endregion

    #region Commands
    public ICommand SaveEmployeeCommand => new Command(SaveEmployee);
    public ICommand BackToEmployeeOverviewCommand => new Command(BackToTestOverview);
    public ICommand GenerateRandomPasswordCommand => new Command(GenerateRandomPassword);

    #endregion

    #region Properties
    
    private string? _status;
    public string? Status
    {
        get { return _status; }
        set { _status = value; OnPropertyChanged(nameof(Status)); }
    }

    private string? _employeeNumber;
    public string? EmployeeNumber
    {
        get { return _employeeNumber; }
        set { _employeeNumber = value; OnPropertyChanged(nameof(EmployeeNumber)); Employee.EmployeeNumber = value; }
    }
    private string? _password;
    public string? Password
    {
        get { return _password; }
        set { _password = value; OnPropertyChanged(nameof(Password)); EmployeeLogin.Password = value; }
    }
    private string? _email;
    public string? Email
    {
        get { return _email; }
        set { _email = value; OnPropertyChanged(nameof(Email)); EmployeeLogin.Email = value; }
    }
    private string? _firstname;
    public string? FirstName
    {
        get { return _firstname; }
        set { _firstname = value; OnPropertyChanged(nameof(FirstName)); Employee.FirstName = value; }
    }

    private string? _lastname;
    public string? LastName
    {
        get { return _lastname; }
        set { _lastname = value; OnPropertyChanged(nameof(LastName)); Employee.LastName = value; }
    }
    private string? _infix;
    public string? Infix
    {
        get { return _infix; }
        set { _infix = value; OnPropertyChanged(nameof(Infix)); Employee.Infix = value; }
    }
    private bool _isAdministrator;
    public bool IsAdministrator
    {
        get { return _isAdministrator; }
        set
        {
            _isAdministrator = value;
            OnPropertyChanged(nameof(IsAdministrator));
            Employee.AccountType = value ? Role.Administrator : Role.Employee;
        }
    }

    #endregion

    #region Errors
    private bool CheckValidityInput()
    {
        // Check the validity of the input
        //string testNameValidation = ErrorService.ValidateInput("TestName", TestName!);
        //string audienceValidation = ErrorService.ValidateInput("Audience", SelectedTargetAudience!);
        string adminValidation = ErrorService.ValidateInput("Admin", navigationStore.LoggedInEmployee, Employee);

        if (!string.IsNullOrEmpty(adminValidation))
        {
            OpenErrorModal(adminValidation);
            return false;
        }

       
        return true;
    }
    #endregion
    private bool PasswordChanged { get; set; }
    public bool IsConfirmed { get; set; }
    public EmployeeLogin EmployeeLogin { get; set; }
    public Employee Employee { get; set; }
    private ConfirmationModalViewModel confirmationModalViewModel { get; set; }

    public EmployeeManagementViewModel(NavigationStore navigationStore, EmployeeLogin? employeeLogin = null, Employee? employee = null)
    {
        //Dependencies initialization
        this.navigationStore = navigationStore;
        this.navigationStore.HideTopBar = true;

        testService = new TestService(new TestRepository());
        employeeService = new EmployeeService(new EmployeeRepository());
        employeeLoginService = new EmployeeLoginService(new EmployeeLoginRepository(), new HashingService.HashingService());

        targetAudienceSerivce = new TargetAudienceService(new TargetAudienceRepository());
       
        if (employeeLogin != null && employee != null)
        {
            Employee = employee;
            EmployeeLogin = employeeLogin;
            SetValues(employeeLogin, employee);
        }
        else
        {
            isNewEmployee = true;
            CreateEmployee();
            SetStatus(false);
        }
    }
    private void SetValues(EmployeeLogin employeeLogin, Employee employee)
    {
        SetFirstName(employee.FirstName);
        SetLastName(employee.LastName);
        SetInfix(employee.Infix);
        SetEmail(employeeLogin.Email);
        SetEmployeeNumber(employee.EmployeeNumber);
        SetIsAdministrator(employee.AccountType == Role.Administrator);
        SetStatus(employeeLogin.Active);
        SetPassword();
    }

    private void SetFirstName(string firstName) => FirstName = firstName;
    private void SetLastName(string lastName) => LastName = lastName;
    private void SetInfix(string infix) => Infix = infix;
    private void SetEmail(string email) => Email = email;
    private void SetPassword() => Password = "***********";
    private void SetEmployeeNumber(string employeeNumber) => EmployeeNumber = employeeNumber;
    private void SetIsAdministrator(bool isAdministrator) => IsAdministrator = isAdministrator;
    private void SetStatus(bool active) => Status = active ? "Actief" : "Inactief";
    private void OpenErrorModal(string text) => navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, text));

    private void CreateEmployee()
    {
        Employee = employeeService.CreateEmployee();
        EmployeeLogin = employeeLoginService.CreateEmployeeLogin();

        EmployeeLogin.Employee = Employee;
        EmployeeLogin.EmployeeId = Employee.Id;
        Employee.EmployeeLogin = EmployeeLogin;
    }
    private void GenerateRandomPassword()
    {
        PasswordChanged = true;
        Password = PasswordService.GeneratePassword();
    }
    public Action CreateAction(Action action)
    {
        return () =>
        {
            if (!IsConfirmed) return;
            action?.Invoke();
        };
    }
    private void BackToTestOverview()
    {
        Action backAction = () =>
        {
            navigationStore!.CurrentViewModel = new EmployeeOverviewViewModel(navigationStore);
        };

        OpenConfirmationModal(CreateAction(backAction), "Weet je zeker dat je terug wilt gaan? Alle wijzigingen zullen ongedaan worden gemaakt.");
    }
    private void SaveEmployee()
    {
        if (!CheckValidityInput())
            return;
        //todo check if password, email, firstname, lastname arent empty.
        employeeService.ProcessEmployee(Employee, isNewEmployee);

        employeeLoginService.ProcessEmployeeLogin(EmployeeLogin, isNewEmployee, PasswordChanged);

    }
    public void OpenConfirmationModal(Action action, string text)
    {
        // Create a confirmation modal view model and open the confirmation modal.
        confirmationModalViewModel = new ConfirmationModalViewModel(navigationStore, text, this, action);
        navigationStore.OpenModal(confirmationModalViewModel);
    }
}
