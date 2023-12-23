using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using UserInterface.ViewModels.Modals;
using static System.Net.Mime.MediaTypeNames;

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
        this.navigationStore.HideTopBar = false;
        // Services
        employeeService = new EmployeeService(new EmployeeRepository(), new EmployeeLoginRepository());

        GetEmployees();
    }

    private void OpenEmployee(Guid id)
    {
        Employee? employee = employeeService.GetEmployeeById(id);
        EmployeeLogin? employeeLogin = employeeService.GetEmployeeLoginById(id);

        if (employee != null && employeeLogin != null)
            navigationStore!.CurrentViewModel = new EmployeeManagementViewModel(navigationStore,employeeLogin,employee);
    }
    private void NewEmployee() => navigationStore!.CurrentViewModel = new EmployeeManagementViewModel(navigationStore);

    private void ToggleActiveStatus(Guid employeeId)
    {
        try
        {
            var clickedEmployee = Employees?.Where(t => t.Id == employeeId).FirstOrDefault();
            if (clickedEmployee == null)
                return;

            employeeService.ToggleActiveStatus(clickedEmployee.Id, navigationStore!.LoggedInEmployee!.Id);
        }
        catch (Exception ex)
        {
            OpenErrorModal(ex.Message);
        }
    }

    private void OpenErrorModal(string text) => navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, text));

    private void DeleteEmployee(Guid id)
    {
        //todo check if employee has any tests.
        if(!employeeService.AbleToDeleteEmployee(id))
        {
            OpenErrorModal("Het verwijderen van deze medewerker is niet toegestaan omdat ze de eigenaar zijn van één of meer testen.");
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
