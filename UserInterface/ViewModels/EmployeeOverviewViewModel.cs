using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using static System.Net.Mime.MediaTypeNames;

namespace UserInterface.ViewModels;

internal class EmployeeOverviewViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore? navigationStore;
    private readonly TestService testService;
    private readonly TargetAudienceService targetAudienceService;
    private readonly EmployeeService employeeService;
    private readonly EmployeeLoginService employeeLoginService;

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

    public EmployeeOverviewViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.navigationStore.AddPreviousViewModel(new EmployeePortalViewModel(navigationStore));
        this.navigationStore.HideTopBar = false;
        // Services
        testService = new TestService(new TestRepository());
        targetAudienceService = new TargetAudienceService(new TargetAudienceRepository());
        employeeService = new EmployeeService(new EmployeeRepository());
        employeeLoginService = new EmployeeLoginService(new EmployeeLoginRepository());

        GetEmployees();
    }

    private void OpenEmployee(Guid id)
    {
        Employee? employee = employeeService.GetEmployeeById(id);
        EmployeeLogin? employeeLogin = employeeLoginService.GetByEmployeeId(id);

        if (employee != null && employeeLogin != null)
            navigationStore!.CurrentViewModel = new EmployeeManagementViewModel(navigationStore,employeeLogin,employee);
    }
    private void NewEmployee() => navigationStore!.CurrentViewModel = new EmployeeManagementViewModel(navigationStore);

    private void ToggleActiveStatus(Guid testId)
    {
        //try
        //{
        //    var clickedTest = Tests?.Where(t => t.Id == testId).FirstOrDefault();
        //    if (clickedTest!.AmountOfQuestions == 0)
        //        return;
        //    testService.ToggleActiveStatus(testId);

        //    if (SelectedTargetAudience == null)
        //        return;

        //    UpdateTestProjections(SelectedTargetAudience.Id);
        //}
        //catch (Exception ex)
        //{

        //}

    }
    private void DeleteEmployee(Guid id)
    {
        //Action SaveAction = () =>
        //{
        //    Test test = testService.GetTestById(id);
        //    testService.DeleteTest(test);
        //    UpdateTestProjections(id);
        //};
        //OpenConfirmationModal(CreateAction(SaveAction), "Weet je zeker dat je deze test wilt verwijderen?");
    }

    private void GetEmployees()
    {
        Employees = employeeService.GetEmployeesProjections();
    }

    public Action CreateAction(Action action)
    {
        throw new NotImplementedException();
    }

    public void OpenConfirmationModal(Action action, string text)
    {
        throw new NotImplementedException();
    }
}
