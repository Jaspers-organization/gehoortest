using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using BusinessLogic.Stores;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using UserInterface.ViewModels.Modals;

namespace UserInterface.ViewModels;

internal class EmployeeOverviewViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore? navigationStore;
    private readonly EmployeeService employeeService;

    #endregion

    #region Commands
    public ICommand OpenEmployeeCommand => new Command(OpenEmployee);
    public ICommand NewEmployeeCommand => new Command(NewEmployee);
    public ICommand DeleteEmployeeCommand => new Command(DeleteEmployee);
    public ICommand ToggleActiveStatusCommand => new Command(ToggleActiveStatus);
    #endregion

    #region properties
    private List<EmployeeProjection>? _employees;
    public List<EmployeeProjection>? Employees
    {
        get { return _employees; }
        set { _employees = value; OnPropertyChanged(nameof(Employees)); }
    }
    public bool IsConfirmed { get; set; }
    #endregion
    private ConfirmationModalViewModel confirmationModalViewModel { get; set; }

    public EmployeeOverviewViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.navigationStore.AddPreviousViewModel(new EmployeePortalViewModel(navigationStore));
        // Services
        employeeService = new EmployeeService(new EmployeeRepository(), new EmployeeLoginRepository());

        GetEmployees();
    }

    private void OpenEmployee(Guid id)
    {
        Employee? employee = employeeService.GetEmployeeById(id);
        EmployeeLogin? employeeLogin = employeeService.GetEmployeeLoginById(id);

        if (employee != null && employeeLogin != null)
            navigationStore!.CurrentViewModel = new EmployeeManagementViewModel(navigationStore, employeeLogin, employee);
    }
    private void NewEmployee() => navigationStore!.CurrentViewModel = new EmployeeManagementViewModel(navigationStore);

    private void ToggleActiveStatus(Guid employeeId)
    {
        var clickedEmployee = Employees?.FirstOrDefault(t => t.Id == employeeId);
        if (clickedEmployee == null)
            return;
        try
        {
            employeeService.ToggleActiveStatus(clickedEmployee.Id, navigationStore!.LoggedInEmployee!.Id);
        }
        catch (Exception ex)
        {
            GetEmployees();
            OpenErrorModal(ex.Message);
        }
    }

    private void OpenErrorModal(string text) => navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, text));

    private void DeleteEmployee(Guid id)
    {
        if (employeeService.CompareEmployeeIds(id, navigationStore.LoggedInEmployee.Id))
        {
            OpenErrorModal(ErrorMessageStore.ErrorDeleteEmployeeSelf);
            return;
        }
        if (!employeeService.AbleToDeleteEmployee(id))
        {
            OpenErrorModal(ErrorMessageStore.ErrorDeleteEmployee);
            return;
        }
        
        Action DeleteAction = () =>
        {
            Employee employee = employeeService.GetEmployeeById(id);
            employeeService.DeleteEmployee(employee);
            GetEmployees();
        };
        OpenConfirmationModal(CreateAction(DeleteAction), "Weet je zeker dat je deze medewerker wilt verwijderen?");
    }

    private void GetEmployees()
    {
        Employees = employeeService.GetEmployeesProjections();
    }

    public Action CreateAction(Action action)
    {
        return () =>
        {
            if (!IsConfirmed) return;
            action?.Invoke();
        };
    }

    public void OpenConfirmationModal(Action action, string text)
    {
        confirmationModalViewModel = new ConfirmationModalViewModel(navigationStore, text, this, action);
        navigationStore.OpenModal(confirmationModalViewModel);
    }
}
