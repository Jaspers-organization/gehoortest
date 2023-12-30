using BusinessLogic.Projections;
using System.Collections.Generic;
using System.Linq;
using UserInterface.Stores;
using System.Windows.Input;
using BusinessLogic.Services;
using UserInterface.Commands;
using BusinessLogic.Interfaces;
using System;
using UserInterface.ViewModels.Modals;
using DataAccess.Repositories;
using BusinessLogic.Models;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore? navigationStore;
    private readonly TestService testService;
    private readonly TargetAudienceService targetAudienceService;
    #endregion

    #region Commands
    public ICommand OpenTestCommand => new Command(OpenTest);
    public ICommand NewTestCommand => new Command(NewTest);
    public ICommand DeleteTestCommand => new Command(DeleteTest);
    public ICommand ToggleActiveStatusCommand => new Command(ToggleActiveStatus);
    #endregion

    #region properties
    private List<TargetAudience>? _targetAudiences;
    public List<TargetAudience>? TargetAudiences
    {
        get { return _targetAudiences; }
        set { _targetAudiences = value; OnPropertyChanged(nameof(TargetAudiences)); }
    }

    private TargetAudience? _selectedTargetAudience;
    public TargetAudience? SelectedTargetAudience
    {
        get { return _selectedTargetAudience; }
        set
        {
            _selectedTargetAudience = value;
            OnPropertyChanged(nameof(SelectedTargetAudience));
            UpdateTestProjections(value!.Id);
        }
    }

    private List<TestProjection>? _tests;
    public List<TestProjection>? Tests
    {
        get { return _tests; }
        set { _tests = value; OnPropertyChanged(nameof(Tests)); }
    }

    public bool IsConfirmed { get; set; }
    #endregion

    public TestOverviewViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.navigationStore.AddPreviousViewModel(new EmployeePortalViewModel(navigationStore));
        this.navigationStore.HideTopBar = false;
        // Services
        testService = new TestService(new TestRepository());
        targetAudienceService = new TargetAudienceService(new TargetAudienceRepository());

        GetTargetAudiences();
    }

    private void GetTargetAudiences()
    {
        TargetAudiences = targetAudienceService.GetAllTargetAudiences();

        if (TargetAudiences == null)
            return;

        SelectedTargetAudience = TargetAudiences.First();
    }

    private void UpdateTestProjections(Guid id) => Tests = testService.GetTestProjectionsByTargetAudienceId(id);

    private void OpenTest(Guid id)
    {
        Test? test = testService.GetTestById(id);

        if (test != null)
            navigationStore!.CurrentViewModel = new TestManagementViewModel(navigationStore, test);
    }
    private void NewTest() => navigationStore!.CurrentViewModel = new TestManagementViewModel(navigationStore);

    private void DeleteTest(Guid id)
    {
        Action SaveAction = () =>
        {
            Test test = testService.GetTestById(id);
            testService.DeleteTest(test);
            UpdateTestProjections(id);
        };
        OpenConfirmationModal(CreateAction(SaveAction), "Weet je zeker dat je deze test wilt verwijderen?");
    }

    public Action CreateAction(Action action)
    {
        return () =>
        {
            if (!IsConfirmed) return;
            action?.Invoke();
        };
    }

    public void OpenConfirmationModal(Action action, string text) => navigationStore.OpenModal(new ConfirmationModalViewModel(navigationStore, text, this, action));
    private void OpenErrorModal(string text) => navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, text));

    private void ToggleActiveStatus(Guid testId)
    {
        try
        {
            var clickedTest = Tests?.Where(t => t.Id == testId).FirstOrDefault();
            if (clickedTest!.AmountOfQuestions == 0)
            {
                OpenErrorModal("Deze test heeft geen vragen. Voeg enkelen vragen toe om hem actief te kunnen zetten.");
                UpdateTestProjections(SelectedTargetAudience.Id);
                return;
            }
                
            testService.ToggleActiveStatus(testId);

            if (SelectedTargetAudience == null)
            {
                UpdateTestProjections(SelectedTargetAudience.Id);
                return;
            }

            UpdateTestProjections(SelectedTargetAudience.Id);
        }
        catch(Exception ex)
        {

        }
    }
}