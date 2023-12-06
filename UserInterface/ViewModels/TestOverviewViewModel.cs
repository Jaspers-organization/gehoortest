using DataAccess.MockData;
using BusinessLogic.IModels;
using BusinessLogic.Projections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UserInterface.Stores;
using System.Windows.Input;
using BusinessLogic.Services;
using UserInterface.Commands;
using BusinessLogic.Interfaces;
using System;
using UserInterface.ViewModels.Modals;
using gehoortest_application.Repository;
using DataAccess.Repositories;
using DataAccess.Models.TestData_Management;

namespace UserInterface.ViewModels;

internal class TestOverviewViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore? navigationStore;
    private readonly TargetAudienceMockRepository targetAudienceRepository;
    private readonly TestMockRepository testRepository;
    private readonly TestService testService;
    private readonly TargetAudienceService targetAudienceService;
    private readonly Repository repository;

    #endregion

    #region Commands
    public ICommand OpenTestCommand => new Command(OpenTest);
    public ICommand NewTestCommand => new Command(NewTest);
    public ICommand DeleteTestCommand => new Command(DeleteTest);
    public ICommand BackToMainMenuCommand => new Command(BackToMainMenu);
    public ICommand ToggleActiveStatusCommand => new Command(ToggleActiveStatus);

    #endregion

    #region properties
    private List<ITargetAudience>? _targetAudiences;
    public List<ITargetAudience>? TargetAudiences
    {
        get { return _targetAudiences; }
        set { _targetAudiences = value; OnPropertyChanged(nameof(TargetAudiences)); }
    }

    private ITargetAudience? _selectedTargetAudience;
    public ITargetAudience? SelectedTargetAudience
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

    public TestOverviewViewModel(NavigationStore navigationStore, Repository repository, ITargetAudience? targetAudience = null)
    {
        this.navigationStore = navigationStore;
        this.repository = repository;

        targetAudienceRepository = new TargetAudienceMockRepository();
        testRepository = new TestMockRepository();

        // Services
        testService = new TestService(testRepository);
        targetAudienceService = new TargetAudienceService(targetAudienceRepository);

        GetTargetAudiences(targetAudience);
    }

    private void GetTargetAudiences(ITargetAudience? targetAudience)
    {
        TargetAudiences = targetAudienceService.GetAllTargetAudiences();

        if (targetAudience != null)
        {
            SelectedTargetAudience = TargetAudiences.First(t => t.Id == targetAudience.Id);
            return;
        }

        SelectedTargetAudience = TargetAudiences.FirstOrDefault();
    }

    private void UpdateTestProjections(int id) => Tests = testService.GetTestProjectionsByTargetAudienceId(id);


    private void BackToMainMenu() => navigationStore!.CurrentViewModel = new TestViewModel(navigationStore, repository);
      

    private void OpenTest(int id)
    {
        ITest test = testService.GetTestById(id);

        if(test != null)
            navigationStore!.CurrentViewModel = new TestManagementViewModel(navigationStore, repository, test);
    }

    private void NewTest() => navigationStore!.CurrentViewModel = new TestManagementViewModel(navigationStore, repository);

    private void DeleteTest(int id)
    {
        Action SaveAction = () =>
        {
            ITest test = testService.GetTestById(id);
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


    private void ToggleActiveStatus(int testId)
    {
        // might be better to move this to the business layer

        testService.ToggleActiveStatus(testId);

        if (SelectedTargetAudience == null)
            return;

        UpdateTestProjections(SelectedTargetAudience.Id);
    }
}
